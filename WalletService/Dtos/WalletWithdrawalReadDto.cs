
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WalletService.Dtos;

public class WalletWithdrawalReadDto
{
    public Guid? Id { get; set; }
    [Precision(18, 2)]
    public decimal? WalletBalance { get; set; }
    [Precision(18, 2)]
    public decimal? WithdrawalBalance { get; set; }
}

