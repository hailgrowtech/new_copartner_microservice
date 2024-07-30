using CommonLibrary.CommonModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MigrationDB.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigrationDB.Model;

[Table("ChatPlan")]
public class ChatPlan :BaseModel
{    
    public Guid ExpertsId { get; set; }
    public Experts Experts { get; set; } // Navigation property
    public string? SubscriptionType { get; set; } 
    public string? PlanType { get; set; } // "F", "P"
    public string? PlanName { get; set; }
    public int? Duration { get; set; } 
    [Precision(18, 2)]
    public decimal ?  Price { get; set; }  
    public int? DiscountPercentage { get; set; } 
    public DateTime? DiscountValidFrom { get; set; }
    public DateTime? DiscountValidTo { get; set; }

}

