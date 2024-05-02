using CommonLibrary.CommonModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigrationDB.Model;

[Table("Wallet")]
public class Wallet : BaseModel
{
    [Required]
    public Guid SubscriberId { get; set; }
    public decimal? RAAmount { get; set; }
    public decimal? APAmount { get; set; }
    public decimal? CPAmount { get; set; }
    public DateTime TransactionDate { get; set; }
}
