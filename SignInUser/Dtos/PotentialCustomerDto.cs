namespace SignInService.Dtos;
public class PotentialCustomerDto
{
    public Guid Id { get; set; }
    public string Mobile { get; set; }
    public string CountryCode { get; set; }
    public string OTP { get; set; }
    public bool IsOTPValidated { get; set; }
    public DateTime OtpExpiryTime { get; set; }
    public string MacAddress { get; set; }
    public string PublicIP { get; set; }
    public int? OTPGenAttemptForMobileCount { get; set; }
    public int CurrentOTPValidationAttempt { get; set; }
    public int? OTPGenAttemptForPublicIpCount { get; set; }
    public string Token { get; set; }

}
