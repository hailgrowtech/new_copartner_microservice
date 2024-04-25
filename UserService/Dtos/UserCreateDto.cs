namespace UserService.Dtos
{
    public class UserCreateDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string SubscriptionType { get; set; }
        public string SubscriptionId { get; set; }

        public string ReferralCode { get; set; } // The referral code used by the AP
        public Guid AffiliatePartnerId { get; set; } // The ID of the AP who referred this user
    }
}
