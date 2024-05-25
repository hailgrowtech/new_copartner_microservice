using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Copartner;

public class WalletEvent
{
    [Required]
    public Guid SubscriberId { get; set; }
    public Guid? AffiliatePartnerId { get; set; }
    public Guid? ExpertsId { get; set; }
    [Precision(18, 2)]
    public decimal? RAAmount { get; set; }
    [Precision(18, 2)]
    public decimal? APAmount { get; set; }
    [Precision(18, 2)]
    public decimal? CPAmount { get; set; }
    public DateTime TransactionDate { get; set; }
}
