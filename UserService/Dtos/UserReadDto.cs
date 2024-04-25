namespace UserService.Dtos;

public class UserReadDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string MobileNumber { get; set; }

    public string ReferralCode { get; set; } // The referral code used by the AP
    public Guid AffiliatePartnerId { get; set; }
}
