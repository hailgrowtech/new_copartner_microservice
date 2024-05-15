using System.ComponentModel.DataAnnotations;

namespace AdminDashboardService.Dtos
{
    public class AdAgencyDetailsDto
    {
        public Guid Id { get; set; }
        public DateTime? JoinDate { get; set; }
        [Required]
        public string AgencyName { get; set; }
        [Required]
        public string? Link { get; set; }
        public int? UsersCount { get; set; }
        public bool isActive { get; set; }
    }
}
