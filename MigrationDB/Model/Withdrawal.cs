using CommonLibrary.CommonModels;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigrationDB.Model;

[Table("Withdrawal")]
public class Withdrawal : BaseModel
{
    [Required]
    public string WithdrawalBy { get; set; } = "RA"; //RA, AP 
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
