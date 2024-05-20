using MigrationDB.Models;

namespace SubscriptionService.Dtos
{
    public class SubscriptionCreateDto
    {
       // public Experts Experts { get; set; }
        public Guid ExpertsId { get; set; }
        public string ImagePath { get; set; }
        public string ServiceType { get; set; }
        public string PlanType { get; set; }
        public int DurationMonth { get; set; }
        public decimal Amount { get; set; }
        public string PremiumTelegramLink { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
