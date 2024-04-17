using CommonLibrary.CommonDTOs;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SignInService.Dtos;
using SignInService.Logic;

namespace SignInService;

[Route("api/[controller]")]
[ApiController]
public class SignInController : ControllerBase
{
    private readonly ISignInBusinessProcessor _signUpBusinessProcessor;
    private readonly ILogger<SignInController> _logger;

    public SignInController(ISignInBusinessProcessor signUpBusinessProcessor, ILogger<SignInController> logger)
    {
        this._signUpBusinessProcessor = signUpBusinessProcessor;
        this._logger = logger;
    }

    /// <summary>
    /// Generate OTP for given number.
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST : api/SignIn/GenerateOTP
    ///     {
    ///         "countryCode": "IN",
    ///         "mobileNumber": "string",
    ///         "otp": "string " Leave Blank or send any string/Number
    ///     }
    /// </remarks>
    /// /// <param name="request"></param>
    /// <returns>Returns Response DTO with MobileValidationDto in Data Property. </returns>
    /// <response code="200">Action Complete</response>
    [HttpPost("GenerateOTP")]
    [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status200OK)]
    public async Task<object> GenerateOTP(MobileValidationDto request)
    {
        _logger.LogInformation("SignUpController : Execute Otp Generation Process..");
        var result = await _signUpBusinessProcessor.ExecuteOtpGenerationProcess(request);
        return Ok(result);

    }

    /// <summary>
    /// Validate OTP for given number.
    /// </summary>
     /// <remarks>
    /// Sample request: Validate OTP
    /// 
    ///     POST : api/SignIn/GenerateOTP
    ///     {
    ///         "countryCode": "IN",
    ///         "mobileNumber": "string",
    ///         "otp": "string" OTP received on Mobile
    ///     }
    /// </remarks>
    /// <param name="MobileValidationDto"></param>
    /// 
    [HttpPost("ValidateOTP")]
    public async Task<object> ValidateOTP(MobileValidationDto request)
    {
        _logger.LogInformation("SignInController : Validate OTP ..");
        var result = await _signUpBusinessProcessor.ValidateOTP(request);
        return Ok(result);
    }
}