using CommonLibrary.CommonModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigrationDB.Models;

[Table("CourseBooking")]
public class CourseBooking : BaseModel
{
    public User User { get; set; }
    public Guid UserId { get; set; }
    public Course Course { get; set; }
    public Guid CourseId { get; set; }
    public DateTime BookingDate { get; set; }
    //public Experts Experts { get; set; }
    //public Guid ExpertsId { get; set; }

}
