using CommonLibrary.CommonModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace MigrationDB.Model;

[Table("FreeChat")]
public class FreeChat:BaseModel
{
    [Required]
    public Guid UserId { get; set; }
   [Required]
    public Guid ExpertsId { get; set; }
    [Required]
    public string PlanType { get; set; }
    public bool Availed { get; set; } = false; // Default value of false
}

