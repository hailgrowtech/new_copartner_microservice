using AuthenticationService.Commands;
using AuthenticationService.Data;
using AuthenticationService.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Handlers;
public class ResetUserAuthDetailsHandler : IRequestHandler<ResetUserAuthCommand, bool>
{
    private readonly AuthenticationDbContext _dbContext;
    public ResetUserAuthDetailsHandler(AuthenticationDbContext dbContext) => _dbContext = dbContext;
    public async Task<bool> Handle(ResetUserAuthCommand request, CancellationToken cancellationToken)
    {
        var entity = request.User;
        var authDetails = await _dbContext.AuthenticationDetails.FirstOrDefaultAsync(a => a.UserId == entity.Id);

        if (authDetails == null)
        {
            return false; // Authentication details not found
        }
        string salt = BCrypt.Net.BCrypt.GenerateSalt();
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(entity.NewPassword, salt);
        // Update password hash in AuthenticationDetails table
        authDetails.PasswordHash = passwordHash;
        await _dbContext.SaveChangesAsync(cancellationToken);

        // Update salt in Authentication table (if needed)
        var auth = await _dbContext.Authentications.FirstOrDefaultAsync(a => a.UserId == entity.Id);
        if (auth != null)
        {
            auth.PasswordSalt = salt;
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        return true; // Password updated successfully
    }
}
