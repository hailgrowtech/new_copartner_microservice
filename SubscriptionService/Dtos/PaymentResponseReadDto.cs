using CommonLibrary.CommonModels;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Model;
using MigrationDB.Models;

namespace SubscriptionService.Dtos;
public class PaymentResponseReadDto
{
    public Guid Id { get; set; }
    public Subscription Subscriptions { get; set; }
    public Guid SubscriptionId { get; set; }
    public User Users { get; set; }
    public Guid UserId { get; set; }
    public string TransactionId { get; set; }
    public string Status { get; set; }
    [Precision(18, 2)]
    public decimal Amount { get; set; }
    public string? PaymentMode { get; set; }
    public DateTime TransactionDate { get; set; }
}
