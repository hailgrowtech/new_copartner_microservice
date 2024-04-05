using AuthenticationService.Models;

namespace AuthenticationService.JWToken;

public interface IJwtUtils
{
    public string GenerateJwtToken(Authentication user);
    public RefreshToken GenerateRefreshToken(string ipAddress);

}
