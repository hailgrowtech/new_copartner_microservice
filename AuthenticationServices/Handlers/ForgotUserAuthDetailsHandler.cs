using System;
using System.Threading;
using System.Threading.Tasks;
using AuthenticationService.Commands;
using AuthenticationService.Data;
using EmailService.Logic;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Handlers
{
    public class ForgotUserAuthDetailsHandler : IRequestHandler<ForgotPasswordCommand, bool>
    {
        private readonly AuthenticationDbContext _dbContext;
        private readonly IEmailBusinessProcessor _emailService;

        public ForgotUserAuthDetailsHandler(AuthenticationDbContext dbContext, IEmailBusinessProcessor emailService)
        {
            _dbContext = dbContext;
            _emailService = emailService;
        }

        public async Task<bool> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
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


            var token = Guid.NewGuid().ToString(); // Generate a unique token
            user.PasswordResetToken = token;
            user.PasswordResetTokenExpiry = DateTime.UtcNow.AddHours(1); // Token valid for 1 hour

            await _dbContext.SaveChangesAsync(cancellationToken);

            // Send email with the reset token
            var resetLink = $"https://copartner.in/forgot-password?token={token}";
            await _emailService.SendEmailAsync(user.Email, "Password Reset", $"Reset your password using this link: {resetLink}");

            return true;
        }
    }
}
