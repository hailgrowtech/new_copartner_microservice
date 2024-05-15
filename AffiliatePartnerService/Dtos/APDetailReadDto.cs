using Microsoft.EntityFrameworkCore;

namespace AffiliatePartnerService.Dtos
{
    public class APDetailReadDto
    {
        public Guid? Id { get; set; }
        public DateTime JoinDate { get; set; }
        public string APName { get; set; }
        public string? MobileNumber { get; set; }
        public int? FixCommission1 { get; set; }
        public int? FixCommission2 { get; set; }
        [Precision(18, 2)]
        public decimal? APEarning { get; set; }
    }
}
