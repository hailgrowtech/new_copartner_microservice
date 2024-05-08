using System.ComponentModel.DataAnnotations;

namespace AdminDashboardService.Dtos
{
    public class ExpertsAdAgencyDto
    {
        [Required]
        public Guid AdvertisingAgencyId { get; set; }
     
        public Guid? ExpertsId { get; set; }
        public Guid? ExpertsAdAgencyId { get; set; }
        public string? ExpertsName { get; set; }

        [Required]
        public string? Link { get; set; }

        public int? UsersCount { get; set; }
        public bool isActive { get; set; }
    }
}
