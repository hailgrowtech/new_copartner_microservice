using CommonLibrary.CommonModels;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigrationDB.Model;
public class PaymentResponse : BaseModel
{
    public Subscription Subscriptions { get; set; }
    public Guid SubscriptionId { get; set; }  
    public User Users { get; set; }
    public Guid UserId { get; set; }
    public string TransactionId { get; set; }
    public string Status { get; set; }
    [Precision(18, 2)]
    public decimal Amount { get; set; }
    public string?  PaymentMode { get; set; }
    public DateTime TransactionDate { get; set; }
    public string? Remarks { get; set; }
}
