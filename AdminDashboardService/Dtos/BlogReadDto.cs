
namespace AdminDashboardService.Dtos;

public class BlogReadDto
{
    public Guid Id { get; set; }
    public DateTime CreatedOn { get; set; }
    public string Title { get; set; }
    public string? BlogImagePath { get; set; }
    public string? Description { get; set; }
}
