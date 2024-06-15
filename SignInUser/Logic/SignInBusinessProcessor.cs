using AutoMapper;
using CommonLibrary;
using CommonLibrary.CommonDTOs;
using CommonLibrary.ExceptionHandler;
using MediatR;
using Newtonsoft.Json.Linq;
using Publication.Factory;
using SignInService.Commands;
using SignInService.Dtos;
using SignInService.JWToken;
using SignInService.Models;
using SignInService.Queries;
using System.Net;
using static MassTransit.ValidationResultExtensions;

namespace SignInService.Logic;
public class SignInBusinessProcessor : ISignInBusinessProcessor
{
    private readonly ISender _sender;
    private readonly IJwtUtils _jwtUtils;
    private readonly IMapper _mapper;
    protected ResponseDto _response;
    private readonly AppSettings _appSettings;

    public SignInBusinessProcessor(ISender sender, IJwtUtils jwtUtils, IMapper mapper, AppSettings appSettings)
    {
        _sender = sender;
        _jwtUtils = jwtUtils;
        _mapper = mapper;
        _response = new ResponseDto();
        _appSettings = appSettings;
    }

    public async Task<ResponseDto> ExecuteOtpGenerationProcess(MobileValidationDto request)
    {
        // Cleanup Data
        request.MobileNumber = request.MobileNumber.Trim();
        ResponseDto response = new ResponseDto();

        bool isNumberValid = MobileHelper.IsMobileNumberValid(request.MobileNumber, request.CountryCode.ToString());

        if (!isNumberValid)
        {
            response.IsSuccess = false;
            response.ErrorMessages = new List<string>() { AppConstants.SignIn_InvalidMobileNumber };
            return response;
        }

        bool isNumberTypeMobileNumber =
            MobileHelper.IsNumberTypeMobile(request.MobileNumber, request.CountryCode.ToString()).ToString().Contains(PhoneNumbers.PhoneNumberType.MOBILE.ToString()) ? true : false;

        if (!isNumberTypeMobileNumber)
        {
            response.IsSuccess = false;
            response.ErrorMessages = new List<string>() { AppConstants.SignIn_InvalidMobileNumber };
            return response;
        }

        int OTP = OtpHelper.GenerateOTP6Digit();

        if (OTP <= 0)
        {
            response.IsSuccess = false;
            response.ErrorMessages = new List<string>() { AppConstants.SignIn_UnableToGenOtp };
            return response;
        }

        //Call fast2SMS to send OTP
        var httpClient = new HttpClient();
        fast2SmsFactory _smsFactory = new fast2SmsFactory(httpClient);
        var numbers = new[] { request.MobileNumber };// { "9999999999", "8888888888", "7777777777" };
        HttpStatusCode statusCode = await _smsFactory.GenerateOTPAsync(numbers, OTP.ToString());
        //HttpStatusCode statusCode = await kaleyraSMS.GenerateOTPAsync(request.MobileNumber, OTP.ToString());
        if (statusCode != HttpStatusCode.OK)//Send OTP to User. ToDO using fast2SMS
        {
            response.IsSuccess = false;
            response.ErrorMessages = new List<string>() { AppConstants.SignIn_UnableToSendOtp };
            return response;
        }

        PotentialCustomer customer = new PotentialCustomer();
        customer.OTP = OTP.ToString();//Change this variable to OTP post fast2SMS implementation.
        customer.CountryCode = request.CountryCode;
        customer.MobileNumber = request.MobileNumber;
        customer.PublicIP = IpHelper.GetUserIp();

        var result = await _sender.Send(new CreateCustomerCommand(customer));

        //Important!! Cleanup OTP before sending response.
        result.OTP = null;
        response.Data = result;
        response.IsSuccess = true;
        response.DisplayMessage = AppConstants.SignIn_OtpGeneratedSucess;
        return response;
    }

    public async Task<ResponseDto> ValidateOTP(MobileValidationDto request)
    {
        PotentialCustomer customerRequest = new PotentialCustomer
        {
            OTP = request.OTP,
            MobileNumber = request.MobileNumber
        };

        PotentialCustomer result = await _sender.Send(new ValidateCustomerCommand(customerRequest));

        if (result == null)
        {
            return new ResponseDto
            {
                IsSuccess = false,
                ErrorMessages = new List<string> { AppConstants.SignIn_NoOTPFoundInDB }
            };
        }

        if (result.IsOTPValidated == true)
        {
            return new ResponseDto
            {
                IsSuccess = false,
                ErrorMessages = new List<string> { AppConstants.SignIn_OTPAlreadyUsed }
            };
        }

        if (result.OTP != request.OTP)
        {
            return new ResponseDto
            {
                IsSuccess = false,
                ErrorMessages = new List<string> { AppConstants.SignIn_InvalidOTP }
            };
        }

        if (result.OtpExpiryTime <= DateTime.UtcNow)
        {
            return new ResponseDto
            {
                IsSuccess = false,
                ErrorMessages = new List<string> { AppConstants.SignIn_OTPExpired }
            };
        }

        // Mark OTP as validated and save it in the database
        result.IsOTPValidated = true;
        result = await _sender.Send(new ValidateCustomerCommand(result, true));

        // Generate JWT token and refresh token
        var jwtToken = _jwtUtils.GenerateJwtToken(result.Id);
        var refreshToken = _jwtUtils.GenerateRefreshToken(IpHelper.GetUserIp());

        // Update user's refresh tokens in the database
        result = await _sender.Send(new UpdateUserTokensCommand(refreshToken, result, _appSettings.RefreshTokenTTL));

        // Map the result to PotentialCustomerDto and include the tokens
        var userDto = _mapper.Map<PotentialCustomerDto>(result);
        userDto.Token = jwtToken;
        userDto.RefreshToken = refreshToken.Token;

        //// Prepare the response
        //var authResponseDto = new AuthResponseDto
        //{
        //    User = userDto,
        //    RefreshToken = refreshToken.Token
        //};

        return new ResponseDto
        {
            IsSuccess = true,
            Data = userDto,
            DisplayMessage = AppConstants.SignIn_OtpValidateSucess
        };
    }
    public async Task<ResponseDto> RefreshToken(string token)
    {
        string ipAddress = IpHelper.GetUserIp();
        var user = await _sender.Send(new GetUserAuthQuery(null, token));
        if (user == null)
            throw new AppException(AppConstants.Auth_InvalidToken);
        var refreshToken = user.RefreshTokens.Single(x => x.Token == token);
        string hostName = Dns.GetHostName();
        var dd = Dns.GetHostEntry(hostName).AddressList[0].ToString();
        if (refreshToken.IsRevoked)
        {
            revokeDescendantRefreshTokens(refreshToken, user, $"Attempted reuse of revoked ancestor token: {token}");
        }

        if (!refreshToken.IsActive)
            throw new AppException(AppConstants.Auth_InvalidToken);

        // replace old refresh token with a new one (rotate token)
        var newRefreshToken = rotateRefreshToken(refreshToken, ipAddress);

        user = await _sender.Send(new UpdateUserTokensCommand(newRefreshToken, user, _appSettings.RefreshTokenTTL));
        if (user == null)
        {
            throw new AppException(AppConstants.Auth_InvalidToken);
        }

        // generate new jwt
        var jwtToken = _jwtUtils.GenerateJwtToken(user.Id);

         var result = await _sender.Send(new GetUserDetailsByIdQuery(user.Id));
        var userDto = _mapper.Map<PotentialCustomerDto>(result);
        userDto.Token = jwtToken;
        userDto.RefreshToken = refreshToken.Token;

        return new ResponseDto()
        {
            IsSuccess = true,
            Data = userDto,
            DisplayMessage = AppConstants.Auth_LoginSucess
        };
    }
    private void revokeDescendantRefreshTokens(RefreshToken refreshToken, PotentialCustomer user, string reason)
    {
        string ipAddress = IpHelper.GetUserIp();
        // recursively traverse the refresh token chain and ensure all descendants are revoked
        if (!string.IsNullOrEmpty(refreshToken.ReplacedByToken))
        {
            var childToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken.ReplacedByToken);
            if (childToken.IsActive)
                revokeRefreshToken(childToken, ipAddress, reason);
            else
                revokeDescendantRefreshTokens(childToken, user, reason);
        }
    }
    private void revokeRefreshToken(RefreshToken token, string ipAddress, string reason = null, string replacedByToken = null)
    {
        token.Revoked = DateTime.UtcNow;
        token.RevokedByIp = ipAddress;
        token.ReasonRevoked = reason;
        token.ReplacedByToken = replacedByToken;
    }
    private RefreshToken rotateRefreshToken(RefreshToken refreshToken, string ipAddress)
    {
        var newRefreshToken = _jwtUtils.GenerateRefreshToken(ipAddress);
        revokeRefreshToken(refreshToken, ipAddress, AppConstants.Auth_RevokeWithReplacement, newRefreshToken.Token);
        return newRefreshToken;
    }
}