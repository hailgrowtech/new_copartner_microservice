using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FeaturesService.Dtos;

public class ChatUserCreateDto
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public string UserType { get; set; }
    public string Username { get; set; }
}
