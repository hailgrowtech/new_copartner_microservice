using CommonLibrary.CommonModels;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MigrationDB.Model
{
    public class WebinarMst : BaseModel
    {
        [Required]
        public string WebinarName { get; set; }
        public Guid? ExpertId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? Duration {  get; set; }
        [Precision(18, 2)]
        public decimal? Amount { get; set; }
        public string? Description { get; set; }
        public string? WebinarLink { get; set; }  
    }
}
