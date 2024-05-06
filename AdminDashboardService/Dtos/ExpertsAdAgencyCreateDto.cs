using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace AdminDashboardService.Dtos
{
    public class ExpertsAdAgencyCreateDto
    {
        [Required]
        public Guid AdvertisingAgencyId { get; set; }
        [Required]
        public Guid ExpertsId { get; set; }
        [Required]
        public string? Link { get; set; }
        public bool isActive { get; set; }
    }
}
