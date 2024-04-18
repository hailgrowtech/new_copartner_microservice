using CommonLibrary.CommonModels;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigrationDB.Model
{
    [Table("Subscriber")]
    public class Subscriber : BaseModel
    {
        public Subscription Subscription { get; set; }
        public Guid SubscriptionId { get; set; }

        public User User { get; set; }
        public Guid UserId { get; set; }

        [Precision(18, 2)]
        public decimal GSTAmount { get; set; }
        [Precision(18, 2)]
        public decimal TotalAmount { get; set; }
        public string PaymentMode { get; set; }
        public string? TransactionId { get; set; }
        public DateTime? TransactionDate { get; set; }

        public bool isActive { get; set; }

    }
}
