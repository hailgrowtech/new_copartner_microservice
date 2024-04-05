using AuthenticationService.Logic;

namespace AuthenticationService.JWToken;
public class JwtMiddleware
{
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IJwtUtils jwtUtils, IAuthenticationBusinessProcessor authenticationBusinessProcessor)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        Guid? userId = CommonLibrary.JwTokenHelper.ValidateJwtToken(token);
        if (userId != null)
        {
            // attach user to context on successful jwt validation
            context.Items["User"] = userId.Value;
        }
        await _next(context);
    }
}