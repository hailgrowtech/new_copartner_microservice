
namespace AdminDashboardService.Dtos;

public class EmailCreateDto
{
    public string  EmailFrom { get; set; }
    public string? To { get; set; }
    public string? Cc { get; set; }
    public string? Subject { get; set; }
    public string? Body { get; set; }
}
