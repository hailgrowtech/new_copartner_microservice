using CommonLibrary.CommonModels;
using MigrationDB.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseService.Models
{
    [Table("CourseStatus")]
    public class CourseStatus : BaseModel
    {
        public Course Course { get; set; }
        public Guid CourseId { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
        public Experts Experts { get; set; }
        public Guid ExpertId { get; set; }
        public int VideoNo { get; set; }
        public bool IsComplete { get; set; }
    }
}
