using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FeaturesService.Dtos;

public class ChatPlanCreateDto
{
    public Guid ExpertsId { get; set; }
    public string? SubscriptionType { get; set; }
    public string? PlanType { get; set; } // "F", "P"
    public string? PlanName { get; set; }
    public int? Duration { get; set; }
    [Precision(18, 2)]
    public decimal? Price { get; set; }
    public int? DiscountPercentage { get; set; }
    public DateTime? DiscountValidFrom { get; set; }
    public DateTime? DiscountValidTo { get; set; }
}
