using AuthenticationService.Commands;
using AuthenticationService.Data;
using AuthenticationService.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Razorpay.Api;

namespace AuthenticationService.Handlers;
public class ResetForgotPasswordHandler : IRequestHandler<ResetForgotPasswordCommand, bool>
{
    private readonly AuthenticationDbContext _dbContext;
    public ResetForgotPasswordHandler(AuthenticationDbContext dbContext) => _dbContext = dbContext;
    public async Task<bool> Handle(ResetForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var entity = request.ResetPassword;

        // Validate Token
        var tokenDetails = await _dbContext.ForgotPasswords
            .Where(fp => fp.Token == entity.Token && fp.Expires > DateTime.UtcNow && fp.IsValidated ==  false)
            .FirstOrDefaultAsync(cancellationToken);

        if (tokenDetails == null)
        {
            return false; // Token is invalid, expired, or already validated
        }

        var authDetails = await _dbContext.AuthenticationDetails.FirstOrDefaultAsync(a => a.UserId == tokenDetails.UserId);

        if (authDetails == null)
        {
            return false; // Authentication details not found
        }
        string salt = BCrypt.Net.BCrypt.GenerateSalt();
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(entity.Password, salt);
        // Update password hash in AuthenticationDetails table
        authDetails.PasswordHash = passwordHash;
        await _dbContext.SaveChangesAsync(cancellationToken);

        // Update salt in Authentication table (if needed)
        var auth = await _dbContext.Authentications.FirstOrDefaultAsync(a => a.UserId == tokenDetails.UserId);
        if (auth != null)
        {
            auth.PasswordSalt = salt;
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        return true; // Password updated successfully
    }
}
