using CommonLibrary.CommonModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigrationDB.Model;

[Table("WithdrawalMode")]
public class WithdrawalMode : BaseModel
{
    [Required]
    public string PaymentMode { get; set; } = "Bank";  //Bank OR UPI
    public Guid? AffiliatePartnerId { get; set; }
    public Guid? ExpertsId { get; set; }
    public string? AccountHolderName { get; set; }
    public string? AccountNumber { get; set; }
    [StringLength(11)]
    public string? IFSCCode { get; set; }
    public string? BankName { get; set; }
    public string? UPI_ID { get; set; }

}
