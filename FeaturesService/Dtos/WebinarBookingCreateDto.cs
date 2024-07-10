using System.ComponentModel.DataAnnotations;

namespace FeaturesService.Dtos
{
    public class WebinarBookingCreateDto
    {
        [Required]
        public Guid? WebinarId { get; set; }
        public Guid? UserId { get; set; }
    }
}
