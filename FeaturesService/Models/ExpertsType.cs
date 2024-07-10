using CommonLibrary.CommonModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FeaturesService.Models;

[Table("ExpertsType")]
public class ExpertsType
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int? Id { get; set; }
    public string? ExpertType { get; set; }
    public string? Description { get; set; }  
}
