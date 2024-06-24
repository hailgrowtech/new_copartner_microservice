using Microsoft.EntityFrameworkCore;
using MigrationDB.Models;

namespace SubscriptionService.Dtos;
public class PaymentResponseCreateDto
{
    public Guid SubscriptionId { get; set; }
    public Guid UserId { get; set; }
    public string TransactionId { get; set; }
    public string Status { get; set; }
    [Precision(18, 2)]
    public decimal Amount { get; set; }
    public string? PaymentMode { get; set; }
    public DateTime TransactionDate { get; set; }
    public string? Remarks  { get; set; }
}
