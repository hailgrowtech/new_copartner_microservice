using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignInService.Models;

[Table("PotentialCustomer")]
public class PotentialCustomer
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string CountryCode { get; set; }
    [Required]
    public string MobileNumber { get; set; }
    public string PublicIP { get; set; }
    public string OTP { get; set; }

    public bool? IsOTPValidated { get; set; }
    public DateTime OtpExpiryTime { get; set; } = DateTime.UtcNow.AddMinutes(5);
    public int? OTPGenAttemptForMobileNumberCount { get; set; }
    public int CurrentOTPValidationAttempt { get; set; }
    public int? OTPGenAttemptForPublicIpCount { get; set; }
    public List<RefreshToken> RefreshTokens { get; set; }
}

