using SignInService.Models;

namespace SignInService.JWToken;

public interface IJwtUtils
{
    public string GenerateJwtToken(Guid id);
    public RefreshToken GenerateRefreshToken(string ipAddress);
}
