using CommonLibrary.CommonModels;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Model;
using MigrationDB.Models;

namespace SubscriptionService.Dtos
{
    public class SubscriptionReadDto : BaseModel
    {
        public Guid Id { get; set; }
        public Experts? Experts { get; set; }
        public Guid? ExpertsId { get; set; }
        public string? ImagePath { get; set; }
        public string? ServiceType { get; set; }
        public string? PlanType { get; set; }
        public string? ChatId { get; set; }
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
        public bool? IsSpecialSubscription { get; set; }
        public bool? isCustom { get; set; }
        public bool isActive { get; set; }

        public static implicit operator SubscriptionReadDto(Subscription v)
        {
            throw new NotImplementedException();
        }
    }
}
