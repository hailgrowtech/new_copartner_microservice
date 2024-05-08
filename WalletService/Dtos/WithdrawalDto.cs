﻿
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WalletService.Dtos;
public class WithdrawalDto
{
    public Guid Id { get; set; }
    [Required]
    public string WithdrawalBy { get; set; } = "RA"; //RA, AP 
    [Precision(18, 2)]
    public decimal? Amount { get; set; }
    public Guid? WithdrawalModeId { get; set; }
    public DateTime? WithdrawalRequestDate { get; set; }  //Date When Withdrawal Request
    [Required]
    public bool isApproved { get; set; } = false;
    public DateTime? TransactionDate { get; set; }
}
public class WithdrawalModeDto
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
