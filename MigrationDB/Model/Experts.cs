using CommonLibrary.CommonModels;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigrationDB.Models;
[Table("Experts")]
public class Experts : BaseModel
{

    [Required]
    public string Name { get; set; }
    public string? LegalName { get; set; }
    //public ExpertsType ExpertType { get; set; }
    public string? ExpertImagePath { get; set; }
    public int? ExpertTypeId { get; set; }
    public string? SEBIRegNo { get; set; }
    public string? Email { get; set; }
    [Precision(18, 2)]
    public decimal? Experience { get; set; }
    [Precision(18, 2)]
    public decimal? Rating { get; set; }
    public string? MobileNumber { get; set; }
    public string? ChannelName { get; set; }
    public string ChatId { get; set; }
    public string? PAN { get; set; }
    public string? Address { get; set; }
    public string? State { get; set; }
    public string? SignatureImage { get; set; }
    public string? GST { get; set; }
    public string? TelegramChannel { get; set; }
    public string? PremiumTelegramChannel1 { get; set; }
    public string? PremiumTelegramChannel2 { get; set; }
    public string? PremiumTelegramChannel3 { get; set; }
    public int? TelegramFollower { get; set; }
    public bool isCoPartner { get; set; }
    public int? FixCommission { get; set; }
    public string? SEBIRegCertificatePath { get; set; }
    public Guid? RelationshipManagerId { get; set; }
    public bool isActive { get; set; }

}
