using CommonLibrary.CommonModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigrationDB.Models;

[Table("CourseStatus")]
public class CourseStatus : BaseModel
{
    public CourseBooking CourseBooking { get; set; }
    public Guid CourseBookingId { get; set; }
    public int VideoNo { get; set; }
    public bool IsComplete { get; set; }
}
