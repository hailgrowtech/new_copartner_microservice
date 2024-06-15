namespace SignInService.Dtos;

public class AuthResponseDto
{
    public PotentialCustomerDto User { get; set; }
    public string RefreshToken { get; set; }
}
