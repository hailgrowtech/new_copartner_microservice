using Microsoft.EntityFrameworkCore;

namespace ExpertsService.Dtos
{
    public class RADetailsDto
    {
        public Guid? Id { get; set; }
        public DateTime JoinDate { get; set; }
        public string Name {  get; set; }
        public string? SEBINo {  get; set; }
        public int? FixCommission { get; set; }
        [Precision(18, 2)]
        public decimal? RAEarning {  get; set; }
        public bool isActive { get; set; }
        public bool isCoPartner { get; set; }
    }

}
