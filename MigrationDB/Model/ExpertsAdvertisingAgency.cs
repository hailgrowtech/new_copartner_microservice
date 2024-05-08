using CommonLibrary.CommonModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigrationDB.Model;

[Table("ExpertsAdvertisingAgency")]
public class ExpertsAdvertisingAgency : BaseModel
{
    [Required]
    public Guid AdvertisingAgencyId { get; set; }
    [Required]
    public Guid ExpertsId { get; set; }
    [Required]
    public string? Link { get; set; }
    public bool isActive { get; set; }
}