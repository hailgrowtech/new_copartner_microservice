using System.ComponentModel.DataAnnotations;

namespace AdminDashboardService.Dtos
{
    public class ExpertsAdAgencyReadDto
    {
        public Guid Id { get; set; }
        [Required]
        public Guid AdvertisingAgencyId { get; set; }
        [Required]
        public Guid ExpertsAdAgencyId { get; set; }
        [Required]
        public Guid ExpertsId { get; set; }

        [Required]
        public string? Link { get; set; }
        public bool isActive { get; set; }
    }
}
