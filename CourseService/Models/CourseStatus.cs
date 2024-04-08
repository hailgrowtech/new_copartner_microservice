using CommonLibrary.CommonModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseService.Models
{
    [Table("CourseStatus")]
    public class CourseStatus : BaseModel
    {
        public int CourseId { get; set; }
        public int UserId { get; set; }
        public int ExpertId { get; set; }
        public int VideoNo { get; set; }
        public bool IsComplete { get; set; }
    }
}
