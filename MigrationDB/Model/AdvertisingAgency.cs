using CommonLibrary.CommonModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigrationDB.Models;

[Table("AdvertisingAgency")]
public class AdvertisingAgency : BaseModel
{
    [Required]
    public string AgencyName { get; set; }
    [Required]
    public string? Link { get; set; }
    public bool isActive { get; set; }
}
