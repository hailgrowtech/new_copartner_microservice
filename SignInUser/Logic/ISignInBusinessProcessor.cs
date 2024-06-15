using CommonLibrary.CommonDTOs;
using SignInService.Dtos;

namespace SignInService.Logic;
public interface ISignInBusinessProcessor
{
    Task<ResponseDto> ExecuteOtpGenerationProcess(MobileValidationDto request);
    Task<ResponseDto> ValidateOTP(MobileValidationDto request);
    Task<ResponseDto> RefreshToken(string token);
}
