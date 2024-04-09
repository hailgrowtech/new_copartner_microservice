using CommonLibrary.CommonModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigrationDB.Models;

[Table("Course")]
public class Course : BaseModel
{
    [Required]
    public string CourseName { get; set; }
    public int VideoCount { get; set; }
    public DateTime Duration { get; set; }
    public DateTime LaunchDate { get; set; }
    public Experts Experts { get; set; }
    public Guid ExpertsId { get; set; }
    public int Amount { get; set; }
}
