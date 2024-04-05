using AuthenticationService.Commands;
using AuthenticationService.DTOs;
using AuthenticationService.JWToken;
using AuthenticationService.Models;
using AuthenticationService.Queries;
using CommonLibrary;
using CommonLibrary.CommonDTOs;
using CommonLibrary.ExceptionHandler;
using MediatR;
using Microsoft.Extensions.Options;
using System.Net;

namespace AuthenticationService.Logic;
public class AuthenticationBusinessProcessor : IAuthenticationBusinessProcessor
{
    private readonly ISender _sender;
    private readonly IJwtUtils _jwtUtils;
    private readonly AppSettings _appSettings;

    public AuthenticationBusinessProcessor(IOptions<AppSettings> appSettings, ISender sender, IJwtUtils jwtUtils)
    {
        this._sender = sender;
        this._jwtUtils = jwtUtils;
        this._appSettings = appSettings.Value;
    }
    public string GetUserIp()
    {
        return IpHelper.GetUserIp();
    }

    public async Task<bool> SaveUserAuthDetails(AuthenticationDetail userAuthDetails)
    {
        //  var userobj = _mapper.Map<User>(user);
        var resutl = await _sender.Send(new CreateUserAuthDetailsCommand(userAuthDetails));
        return true;
    }
    public async Task<bool> SaveUserAuth(Authentication userAuth)
    {
        //  var userobj = _mapper.Map<User>(user);
        var resutl = await _sender.Send(new CreateUserAuthCommand(userAuth));
        return true;
    }
    public async Task<ResponseDto> Authenticate(AuthenticationRequestDTO request)
    {
        AuthenticationDetail authDetailsToValidate = new AuthenticationDetail()
        {
            MobileNumber = request.Mobile,
            Email = request.Email
        };

        var authDtls = await _sender.Send(new GetUserAuthDetailsQuery(authDetailsToValidate));

        if (authDtls == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                ErrorMessages = new List<string>() { AppConstants.Auth_Atmpt_NoUserFoundWithUserCred }
            };
        }

        Authentication authRequestToValidate = new Authentication()
        {
            UserId = authDtls.UserId
        };

        var auth = await _sender.Send(new GetUserAuthQuery(authRequestToValidate, null));

        if (auth == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                ErrorMessages = new List<string>() { AppConstants.Auth_Atmpt_NoSaltFoundWithUserCred }
            };
        }

        //Can we use BCrypt.EnhancedVerify here??
        if (!BCrypt.Net.BCrypt.Verify(request.PasswordHash, authDtls.PasswordHash.ToString()))// probably wrong code. Deepak bhai check it. TODO
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                ErrorMessages = new List<string>() { AppConstants.Auth_InvalidUsernamePassword }
            };
        }

        // authentication successful so generate jwt and refresh tokens
        var jwtToken = _jwtUtils.GenerateJwtToken(auth);

        var refreshToken = _jwtUtils.GenerateRefreshToken(IpHelper.GetUserIp());

        // remove old refresh tokens from user and add new one
        auth = await _sender.Send(new UpdateAuthenticationTokensCommand(refreshToken, auth, _appSettings.RefreshTokenTTL));

        return new ResponseDto()
        {
            IsSuccess = true,
            Data = GenerateAuthenticationResponseDTO(auth, authDtls, jwtToken, refreshToken.Token),
            DisplayMessage = AppConstants.Auth_LoginSucess
        };


    }
    public async Task<ResponseDto> RefreshToken(string token)
    {
        string ipAddress = IpHelper.GetUserIp();
        var auth = await _sender.Send(new GetUserAuthQuery(null, token));
        if (auth == null)
            throw new AppException(AppConstants.Auth_InvalidToken);
        var refreshToken = auth.RefreshTokens.Single(x => x.Token == token);
        string hostName = Dns.GetHostName();
        var dd = Dns.GetHostEntry(hostName).AddressList[0].ToString();
        if (refreshToken.IsRevoked)
        {
            revokeDescendantRefreshTokens(refreshToken, auth, $"Attempted reuse of revoked ancestor token: {token}");
        }

        if (!refreshToken.IsActive)
            throw new AppException(AppConstants.Auth_InvalidToken);

        // replace old refresh token with a new one (rotate token)
        var newRefreshToken = rotateRefreshToken(refreshToken, ipAddress);

        auth = await _sender.Send(new UpdateAuthenticationTokensCommand(newRefreshToken, auth, _appSettings.RefreshTokenTTL));
        if (auth == null)
        {
            throw new AppException(AppConstants.Auth_InvalidToken);
        }

        // generate new jwt
        var jwtToken = _jwtUtils.GenerateJwtToken(auth);

        var authDtls = await _sender.Send(new GetUserAuthDetailsByIdQuery(auth.UserId));

        return new ResponseDto()
        {
            IsSuccess = true,
            Data = GenerateAuthenticationResponseDTO(auth, authDtls, jwtToken, newRefreshToken.Token),
            DisplayMessage = AppConstants.Auth_LoginSucess
        };

    }
    private AuthenticationResponseDTO GenerateAuthenticationResponseDTO(Authentication auth, AuthenticationDetail authDetails, string jwtToken, string refreshToken)
    {
        return new AuthenticationResponseDTO()
        {
            Id = auth.UserId,
            Email = authDetails.Email,
            RefreshToken = refreshToken,
            JwtToken = jwtToken
        };
    }
    private void revokeDescendantRefreshTokens(RefreshToken refreshToken, Authentication user, string reason)
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
    private void removeOldRefreshTokens(Authentication auth)
    {
        // remove old inactive refresh tokens from user based on TTL in app settings
        auth.RefreshTokens.RemoveAll(x =>
            !x.IsActive &&
            x.Created.AddDays(_appSettings.RefreshTokenTTL) <= DateTime.UtcNow);
    }
    public async void RevokeToken(string token)
    {
        string ipAddress = IpHelper.GetUserIp();
        var auth = await _sender.Send(new GetUserAuthQuery(null, token));
        if (auth == null)
            throw new AppException(AppConstants.Auth_InvalidToken);

        var refreshToken = auth.RefreshTokens.Single(x => x.Token == token);
        if (!refreshToken.IsActive)
            throw new AppException(AppConstants.Auth_InvalidToken);

        // revoke token and save
        //TODO : nothing is going to dtabase, what are we saving here?
        revokeRefreshToken(refreshToken, ipAddress, AppConstants.Auth_RevokeWithoutReplacement);
        //_context.Update(user);
        //_context.SaveChanges();

    }
    public async Task<ResponseDto> GetAuthUserById(Guid id)
    {
        var auth = await GetAuthUserByIdAsync(id);
        var authDtls = await _sender.Send(new GetUserAuthDetailsByIdQuery(auth.UserId));
        // generate new jwt
        var jwtToken = _jwtUtils.GenerateJwtToken(auth);
        var refreshToken = _jwtUtils.GenerateRefreshToken(IpHelper.GetUserIp());
        return new ResponseDto()
        {
            IsSuccess = true,
            Data = GenerateAuthenticationResponseDTO(auth, authDtls, jwtToken, refreshToken.Token),
            DisplayMessage = AppConstants.Auth_LoginSucess
        };

    }
    protected async Task<Authentication> GetAuthUserByIdAsync(Guid id)
    {
        var auth = await _sender.Send(new GetUserByUserIdQuery(id));
        return auth;
    }

}