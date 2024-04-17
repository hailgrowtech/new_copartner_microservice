using AutoMapper;
using CommonLibrary;
using CommonLibrary.CommonDTOs;
using MediatR;
using Publication.Factory;
using SignInService.Commands;
using SignInService.Dtos;
using SignInService.JWToken;
using SignInService.Models;
using System.Net;

namespace SignInService.Logic;
public class SignInBusinessProcessor : ISignInBusinessProcessor
{
    private readonly ISender _sender;
    private readonly IJwtUtils _jwtUtils;
    private readonly IMapper _mapper;
    protected ResponseDto _response;

    public SignInBusinessProcessor(ISender sender, IJwtUtils jwtUtils, IMapper mapper)
    {
        _sender = sender;
        _jwtUtils = jwtUtils;
        _mapper = mapper;
        _response = new ResponseDto();
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
        PotentialCustomer customerRequest = new PotentialCustomer();
        customerRequest.OTP = request.OTP;
        customerRequest.MobileNumber = request.MobileNumber;

        PotentialCustomer result = await _sender.Send(new ValidateCustomerCommand(customerRequest));

        if (result == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                ErrorMessages = new List<string>() { AppConstants.SignIn_NoOTPFoundInDB }
            };
        }

        if (result.IsOTPValidated == true)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                ErrorMessages = new List<string>() { AppConstants.SignIn_OTPAlreadyUsed }
            };
        }

        if (result.OTP != request.OTP)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                ErrorMessages = new List<string>() { AppConstants.SignIn_InvalidOTP }
            };
        }

        if (result.OtpExpiryTime <= DateTime.UtcNow)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                ErrorMessages = new List<string>() { AppConstants.SignIn_OTPExpired }
            };
        }

        //Mark OTP as validated = true and save it in DB.
        result.IsOTPValidated = true;
        result = await _sender.Send(new ValidateCustomerCommand(result, true));

        var responseDto = _mapper.Map<PotentialCustomerDto>(result);
        responseDto.Token = _jwtUtils.GenerateJwtToken(result.Id);

        return new ResponseDto()
        {
            IsSuccess = true,
            Data = responseDto,
            DisplayMessage = AppConstants.SignIn_OtpValidateSucess
        };
    }
}