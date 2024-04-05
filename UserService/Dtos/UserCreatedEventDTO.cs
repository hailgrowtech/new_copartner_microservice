namespace UserService.Dtos;

public class UserCreatedEventDTO
{
    public Guid UserId { get; set; }
    public string Mobile { get; set; }
    public string Email { get; set; }
}
