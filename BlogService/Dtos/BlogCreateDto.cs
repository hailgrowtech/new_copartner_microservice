using ExpertService.Models;
using System.Numerics;

namespace BlogService.Dtos
{
    public class BLogCreateDto
    {
        public string Title { get; set; }
        public string? BlogImagePath { get; set; }
        public string? Description { get; set; }
    }
}
