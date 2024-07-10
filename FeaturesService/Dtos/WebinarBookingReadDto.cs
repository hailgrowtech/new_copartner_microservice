using System.ComponentModel.DataAnnotations;

namespace FeaturesService.Dtos
{
    public class WebinarBookingReadDto
    {
        public Guid Id { get; set; }
        public Guid? WebinarId { get; set; }
        public Guid? UserId { get; set; }
    }
}
