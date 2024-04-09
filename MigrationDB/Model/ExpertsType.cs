using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MigrationDB.Models;

[Table("ExpertsType")]
public class ExpertsType
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int? Id { get; set; }
    public string? ExpertType { get; set; }
    public string? Description { get; set; }
}
