using CommonLibrary.CommonModels;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigrationDB.Model;

[Table("Wallet")]
public class Wallet : BaseModel
{
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
