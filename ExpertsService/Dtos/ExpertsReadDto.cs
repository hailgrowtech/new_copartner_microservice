using ExpertService.Models;
using System.Numerics;

namespace ExpertService.Dtos;

public class ExpertReadDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ExpertsType ExpertTypeID { get; set; }
    public string? SEBIRegNo { get; set; }
    public string? Email { get; set; }
    public int? Experience { get; set; }
    public int? Rating { get; set; }
    public string? MobileNumber { get; set; }
    public string TelegramChannel { get; set; }
    public int? TelegramFollower { get; set; }
    public bool isCoPartner { get; set; }
}
