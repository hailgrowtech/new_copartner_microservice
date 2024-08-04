using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FeaturesService.Dtos;

public class FreeChatReadDto
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public Guid ExpertsId { get; set; }
    public bool Availed { get; set; }  // Default value of false
}
