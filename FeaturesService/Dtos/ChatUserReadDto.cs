using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FeaturesService.Dtos;

public class ChatUserReadDto
{
    [Required]
    public Guid Id { get; set; }
    public string UserType { get; set; }
    public string Username { get; set; } 
}
