using Microsoft.EntityFrameworkCore;
using MigrationDB.Models;

namespace ExpertsService.Dtos;

public class RAListingDto
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public int UsersCount { get; set; }
    [Precision(18, 2)]
    public decimal? RAEarning {  get; set; }
    [Precision(18, 2)]
    public decimal? CPEarning { get; set; }

}
public class RAListingDataDto
{
    public string RAName { get; set; }
    
    public DateTime? UserJoiningDate { get; set; }
    public DateTime? SubscribeDate { get; set; }
    public string? UserMobileNo { get; set; }
    public User User { get; set; }

    public string? APName { get; set; }
    [Precision(18, 2)]
    public decimal? Amount { get; set; }
    public string Subscription { get; set; }
    public string PlanType { get; set; }
    public string TransactionId { get; set; }
    public decimal? SubscriptionAmount { get; set; }
    public string? LegalName { get; set; }
    public string? GST {  get; set; }
    public string? InvoiceId { get; set; }
    public string? PremiumTelegramChannel { get; set; }
    public string? SignatureImage { get; set; }
    public decimal? GSTAmount { get; set; }
    public decimal? TotalAmount { get; set; }
    public int? DiscountPercentage { get; set; }
    public string? Address { get; set; }
    public string? State { get; set; }
}
