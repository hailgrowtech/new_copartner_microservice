using System.ComponentModel.DataAnnotations;

namespace AdvertisingAgencyService.Dtos
{
    public class AdAgencyDetailsDto
    {
        [Required]
        public string AgencyName { get; set; }
        [Required]
        public string? Link { get; set; }
        public bool isActive { get; set; }
    }
}
