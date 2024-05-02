namespace AffiliatePartnerService.Dtos
{
    public class APListingDto
    {
        public string APName {  get; set; }
        public int UsersCount { get; set; }
        public int UsersPayment { get; set; }
        public decimal? APEarning { get; set; }
        public decimal? RAEarning { get; set; }
        public decimal? CPEarning {  get; set; }
    }
}
