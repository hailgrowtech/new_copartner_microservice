using CommonLibrary.CommonModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseService.Models
{
    [Table("CourseBooking")]
    public class CourseBooking : BaseModel
    {
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public DateTime BookingDate { get; set; }
        public int ExpertId { get; set; }

    }
}
