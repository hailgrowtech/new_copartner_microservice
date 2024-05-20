using CommonLibrary.CommonModels;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigrationDB.Model
{
    [Table("Subscription")]
    public class Subscription : BaseModel
    {
        public Experts Experts { get; set; }
        public Guid ExpertsId { get; set; }
        public string? ImagePath { get; set; }
        public string? Tag { get; set; }
        public string? ServiceType { get; set; }
        public string? PlanType { get; set; }
        public int?  DurationMonth { get; set; }
        [Precision(18, 2)]
        public decimal?  Amount { get; set; }
        public string?  PremiumTelegramLink { get; set; }
        public string?  Description { get; set; }
        public bool isActive { get; set; }
    }
}
