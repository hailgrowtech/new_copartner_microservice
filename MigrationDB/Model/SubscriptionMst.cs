using CommonLibrary.CommonModels;
using MigrationDB.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigrationDB.Model
{
    [Table("SubscriptionMsts")]
    public class SubscriptionMst : BaseModel
    {
        public Experts Experts { get; set; }
        public Guid ExpertsId { get; set; }
        public string ImagePath { get; set; }
        public string Tag { get; set; }
        public string ServiceType { get; set; }
        public string PlanType { get; set; }
        public int  DurationMonth { get; set; }
        public decimal  Amount { get; set; }
        public string  PremiumTelegramLink { get; set; }
        public string  Description { get; set; }
        public bool isActive { get; set; }
    }
}
