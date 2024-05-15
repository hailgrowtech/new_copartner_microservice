
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WalletService.Dtos;

public class WalletReadDto
{
    public Guid Id { get; set; }
    [Required]
    public Guid SubscriberId { get; set; }
    [Precision(18, 2)]
    public decimal? RAAmount { get; set; }
    [Precision(18, 2)]
    public decimal? APAmount { get; set; }
    [Precision(18, 2)]
    public decimal? CPAmount { get; set; }
    public DateTime TransactionDate { get; set; }
}

