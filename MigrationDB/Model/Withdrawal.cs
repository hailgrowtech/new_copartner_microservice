using CommonLibrary.CommonModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigrationDB.Model;

[Table("Withdrawal")]
public class Withdrawal : BaseModel
{
    [Required]
    public string WithdrawalBy { get; set; } = "RA"; //RA, AP 
    public decimal? Amount { get; set; }
    public Guid? WithdrawalPaymentModeId { get; set; }
    public DateTime WithdrawalRequestDate { get; set; }  //Date When Withdrawal Request
    [Required]
    public bool isApproved { get; set; } = false;
    public DateTime TransactionDate { get; set; }
}
