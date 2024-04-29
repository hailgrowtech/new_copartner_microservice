namespace ExpertService.Dtos;

public class ExpertsCreatedEventDTO
{
    public Guid UserId { get; set; }
    public string Mobile { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
