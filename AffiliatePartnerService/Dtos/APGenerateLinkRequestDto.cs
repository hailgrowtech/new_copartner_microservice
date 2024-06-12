namespace AffiliatePartnerService.Dtos
{
    public class APGenerateLinkRequestDto
    {
        public Guid AffiliatePartnerId { get; set; }
        public int Num { get; set; }
        public string APReferralLink { get; set; }
    }
}
