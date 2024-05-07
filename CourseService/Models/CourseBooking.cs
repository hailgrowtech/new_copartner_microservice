using CommonLibrary.CommonModels;
using MigrationDB.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseService.Models
{
    [Table("CourseBooking")]
    public class CourseBooking : BaseModel
    {
        public User User { get; set; }
        public Guid UserId { get; set; }
        public Course Course { get; set; }
        public Guid CourseId { get; set; }
        public DateTime BookingDate { get; set; }
        public Experts Experts { get; set; }
        public Guid ExpertId { get; set; }

    }
}
