using CommonLibrary.CommonModels;
using MigrationDB.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigrationDB.Model
{
    [Table("Subscriber")]
    public class Subscriber : BaseModel
    {
        public SubscriptionMst SubscriptionMst { get; set; }
        public Guid SubscriptionId { get; set; }

        public User User { get; set; }
        public Guid UserId { get; set; }

        public decimal GSTAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentMode { get; set; }
        public bool isActive { get; set; }

    }
}
