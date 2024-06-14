using Microsoft.EntityFrameworkCore;
using MigrationDB.Models;

namespace SubscriptionService.Dtos
{
    public class SubscriptionReadDto
    {
        public Guid Id { get; set; }
        public Experts? Experts { get; set; }
        public Guid? ExpertsId { get; set; }
        public string? ImagePath { get; set; }
        public string? ServiceType { get; set; }
        public string? PlanType { get; set; }
        public int? DurationMonth { get; set; }
        [Precision(18, 2)]
        public decimal? Amount { get; set; }
        public string? PremiumTelegramLink { get; set; }
        public string? Description { get; set; }
        [Precision(18, 2)]
        public decimal? DiscountedAmount { get; set; }
        public int? DiscountPercentage { get; set; }
        public DateTime? DiscountValidFrom { get; set; }
        public DateTime? DiscountValidTo { get; set; }
        public bool isActive { get; set; }
    }
}
