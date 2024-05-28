using CommonLibrary.CommonDTOs;
using Copartner;
using MassTransit;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Razorpay.Api;
using SignInService.Dtos;
using SignInService.Logic;

namespace SignInUserService;

[Route("api/[controller]")]
[ApiController]
public class SignInController : ControllerBase
{
    private readonly ISignInBusinessProcessor _signUpBusinessProcessor;
    private readonly ILogger<SignInController> _logger;
   // private readonly IPublishEndpoint _publish;
    public SignInController(ISignInBusinessProcessor signUpBusinessProcessor, ILogger<SignInController> logger, IPublishEndpoint publish)
    {
        this._signUpBusinessProcessor = signUpBusinessProcessor;
        this._logger = logger;
      //  this._publish = publish;
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
        var response = await _signUpBusinessProcessor.ValidateOTP(request);
        if (response.IsSuccess)
        {
            //_publish.Publish<UserCreatedEvent>(new
            //{
            //    MobileNumber = request.MobileNumber
            //});

            return Ok(response);
        }
        return NotFound(response);
    }
}