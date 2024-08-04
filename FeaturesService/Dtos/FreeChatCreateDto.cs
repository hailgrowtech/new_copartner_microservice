using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FeaturesService.Dtos;

public class FreeChatCreateDto
{
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public Guid ExpertsId { get; set; }
    public bool Availed { get; set; } = false; // Default value of false
}
