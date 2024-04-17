using System.ComponentModel.DataAnnotations;

namespace SignInService.Dtos;

public class MobileValidationDto
{
    [Required]
    public string CountryCode { get; set; } =  CommonLibrary.AppConstants.CountryCode.IN.ToString();
    [Required]
    public string MobileNumber { get; set; }
    public string OTP { get; set; }
}
