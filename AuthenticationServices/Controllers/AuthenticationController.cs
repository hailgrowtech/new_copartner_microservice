using AuthenticationService.DTOs;
using AuthenticationService.Logic;
using CommonLibrary;
using CommonLibrary.Authorization;
using CommonLibrary.CommonDTOs;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Controllers;
/// <summary>
/// Authentication API .
/// </summary>
/// <returns>Authentication API to auth related operations for user.</returns>
// GET: api/Authentication
[Authorize]
[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{

    private readonly IAuthenticationBusinessProcessor _authenticationBusinessProcessor;

    public AuthenticationController(IAuthenticationBusinessProcessor authenticationBusinessProcessor)
    {
        this._authenticationBusinessProcessor = authenticationBusinessProcessor;
    }
    /// <summary>
    /// Authenticate User.
    /// </summary>
    /// <returns>Authenticate user and returns it.</returns>
    // GET: api/Authentication

    [AllowAnonymous]
    [HttpPost("authenticate")]
    public async Task<object> Authenticate(AuthenticationRequestDTO request)
    {
        request.UserIpAddress = ipAddress();
        
        var response = await _authenticationBusinessProcessor.Authenticate(request);
        if (response.IsSuccess == true)
        {
            setTokenCookie(
                new TokenRequestDTO()
                {
                    Token = ((AuthenticationResponseDTO)response.Data).RefreshToken
                });
        }
        return Ok(response);
    }
    /// <summary>
    /// Refresh User's Token.
    /// </summary>
    /// <returns>Returns new Refresh Token</returns>
    // GET: api/Authentication
    [AllowAnonymous]
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken()
    {
        var refreshToken = Request.Cookies[AppConstants.cookies_RefreshToken];
        if (refreshToken != null)
        {
            var response = await _authenticationBusinessProcessor.RefreshToken(refreshToken);

            // Assuming TokenRequestDTO has a property named RefreshToken
            var refreshTokenValue = ((AuthenticationService.DTOs.AuthenticationResponseDTO)response.Data).RefreshToken;

            if (string.IsNullOrEmpty(refreshTokenValue))
            {
                return BadRequest(new { message = "Refresh Token Not Found in Response." });
            }

            setTokenCookie(new TokenRequestDTO { Token = refreshTokenValue });
            return Ok(response);
        }
        else
        {
            return BadRequest(new { message = "Refresh Token Not Found for User." });
        }
    }

    [HttpPost("revoke-token")]
    public async Task<object> RevokeToken(TokenRequestDTO request)
    {
        // accept refresh token in request body or cookie
        var token = request.Token ?? Request.Cookies[AppConstants.cookies_RefreshToken];
        ResponseDto response = new ResponseDto();
        if (string.IsNullOrEmpty(token))
        {
            return BadRequest(new ResponseDto()
            {
                IsSuccess = false,
                ErrorMessages = new List<string>() { AppConstants.Auth_NoToken }
            });
        }
        _authenticationBusinessProcessor.RevokeToken(token);
        return Ok(new ResponseDto()
        {
            DisplayMessage = AppConstants.Auth_TokenRevoked
        });
    }

    [HttpGet("{id}/refresh-tokens")]
    public async Task<IActionResult> GetRefreshTokens(Guid id)
    {
        var auth = await _authenticationBusinessProcessor.GetAuthUserById(id);

        if (auth == null)
        {
            return NotFound(); // Or appropriate error response
        }

        var refreshToken = ((AuthenticationService.DTOs.AuthenticationResponseDTO)auth.Data).RefreshToken;

        if (string.IsNullOrEmpty(refreshToken))
        {
            return NotFound(); // Or appropriate error response
        }

        return Ok(refreshToken);
    }



    // helper methods

    private void setTokenCookie(TokenRequestDTO request)
    {
        // append cookie with refresh token to the http response
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddDays(7)
        };
        Response.Cookies.Append(AppConstants.cookies_RefreshToken, request.Token, cookieOptions);
    }

    private string ipAddress()
    {
        // get source ip address for the current request
        if (Request.Headers.ContainsKey("X-Forwarded-For"))
            return Request.Headers["X-Forwarded-For"];
        else
        {
            return _authenticationBusinessProcessor.GetUserIp();
        }
    }
}
