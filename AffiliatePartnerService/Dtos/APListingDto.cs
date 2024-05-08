using Microsoft.EntityFrameworkCore;

namespace AffiliatePartnerService.Dtos
{
    public class APListingDto
    {
        public string APName {  get; set; }
        public int UsersCount { get; set; }
        public int UsersPayment { get; set; }
        [Precision(18, 2)]
        public decimal? APEarning { get; set; }
        [Precision(18, 2)]
        public decimal? RAEarning { get; set; }
        [Precision(18, 2)]
        public decimal? CPEarning {  get; set; }
    }
}
