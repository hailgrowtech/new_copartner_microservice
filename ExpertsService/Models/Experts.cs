using CommonLibrary.CommonModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace ExpertService.Models;
[Table("Experts")]
public class Experts : BaseModel
{

    [Required]
    public string Name { get; set; }
    public ExpertsType ExpertType { get; set; }
    public string? SEBIRegNo { get; set; }
    public string? Email { get; set; }
    public int? Experience { get; set; }
    public int? Rating { get; set; }
    public string? MobileNumber { get; set; }
    public string TelegramChannel { get; set; }
    public int? TelegramFollower { get; set; }
    public bool isCoPartner { get; set; }
}
