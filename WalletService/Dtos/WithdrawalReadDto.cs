
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WalletService.Dtos;

public class WithdrawalReadDto
{
    public Guid Id { get; set; }
    [Required]
    public string WithdrawalBy { get; set; } = "RA"; //RA, AP     
    public string? SEBINo { get; set; }
    public string? MobileNo { get; set; }
    public string? Name { get; set; }
    [Precision(18, 2)]
    public decimal? Amount { get; set; }
    public Guid? WithdrawalModeId { get; set; }
    public DateTime? WithdrawalRequestDate { get; set; }  //Date When Withdrawal Request
    [Required]
    [MaxLength(1)]
    public string? RequestAction { get; set; }
    public string? TransactionId { get; set; }
    public DateTime? TransactionDate { get; set; }
    [Column(TypeName = "text")]
    public string? RejectReason { get; set; }
}

public class WithdrawalModeReadDto
{
    public Guid Id { get; set; }
    [Required]
    public string PaymentMode { get; set; }  //Bank OR UPI
    public Guid? AffiliatePartnerId { get; set; }
    public Guid? ExpertsId { get; set; }
    public string? AccountHolderName { get; set; }
    public string? AccountNumber { get; set; }
    [StringLength(11)]
    public string? IFSCCode { get; set; }
    public string? BankName { get; set; }
    public string? UPI_ID { get; set; }
}

public class WithdrawalDetailsReadDto
{
    public Guid Id { get; set; }
    [Required]
    public string PaymentMode { get; set; }  //Bank OR UPI
    public string? Name { get; set; }
    [Precision(18, 2)]
    public decimal? Amount { get; set; }
    public string? AccountHolderName { get; set; }
    public string? AccountNumber { get; set; }
    [StringLength(11)]
    public string? IFSCCode { get; set; }
    public string? BankName { get; set; }
    public string? UPI_ID { get; set; }
    public string? RequestAction { get; set; }
}
