using ExpertService.Models;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace AdvertisingAgencyService.Dtos
{
    public class AdAgencyDetailsCreateDto
    {
        [Required]
        public string AgencyName { get; set; }
        [Required]
        public string? Link { get; set; }
        public bool isActive { get; set; }
    }
}
