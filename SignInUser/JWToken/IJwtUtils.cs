namespace SignInService.JWToken;

public interface IJwtUtils
{
    public string GenerateJwtToken(Guid id);
}
