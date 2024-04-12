using MigrationDB.Model;
using MigrationDB.Models;

namespace SubscriptionService.Dtos
{
    public class SubscriberDto
    {
        
        public Guid SubscriptionId { get; set; }


        public Guid UserId { get; set; }

        public decimal GSTAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentMode { get; set; }
        public bool isActive { get; set; }
    }
}
