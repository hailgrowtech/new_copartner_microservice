using Microsoft.EntityFrameworkCore;

namespace AffiliatePartnerService.Dtos
{
    public class APListingDataDto
    {
        public string APName { get; set; }
        public string ReferralLink { get; set; }
        public DateTime? Date { get; set; }
        public string? UserMobileNo { get; set; }
        public string? RAName { get; set; }
        [Precision(18, 2)]
        public decimal? Amount { get; set; }
        public string Subscription { get; set; }
    }
}
