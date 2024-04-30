using Microsoft.EntityFrameworkCore.Query.Internal;

namespace AffiliatePartnerService.Dtos
{
    public class APListingDetailDto
    {
        public DateTime JoinDate { get; set; }
        public string Name { get; set; }
        public string? MobileNumber { get; set; }
        public int? FixCommission1 { get; set; }
        public int? FixCommission2 { get; set; }
        public long Spend {  get; set; }
    }
}
