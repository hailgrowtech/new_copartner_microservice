using Microsoft.EntityFrameworkCore;

namespace ExpertsService.Dtos
{
    public class RAListingDetailsDto
    {
        public DateTime JoinDate { get; set; }
        public string Name {  get; set; }
        public string? SEBINo {  get; set; }
        public int? FixCommission { get; set; }
        [Precision(18, 2)]
        public decimal? RAEarning {  get; set; }
    }
}
