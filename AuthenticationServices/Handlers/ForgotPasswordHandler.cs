using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AuthenticationService.Commands;
using AuthenticationService.Data;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Publication.Factory;

namespace AuthenticationService.Handlers
{
    public class ForgotPasswordHandler : IRequestHandler<ForgotPasswordCommand, HttpStatusCode>
    {
        private readonly AuthenticationDbContext _dbContext;
        private readonly eMailFactory _eMailFactory;
        public ForgotPasswordHandler(AuthenticationDbContext dbContext,eMailFactory eMailFactory)
        {
            _dbContext = dbContext;
            _eMailFactory = eMailFactory;
        }

        public async Task<HttpStatusCode> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var entity = request.ForgotPassword;
            //var userDetails = await _dbContext.ForgotPasswords.FirstOrDefaultAsync(a => a.UserId == entity.UserId && a.);

            //if (userDetails != null)
            //{
            //    return false; // Authentication details alreay found
            //}

            await _dbContext.ForgotPasswords.AddAsync(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            // Send email with the reset token
            var resetLink = $"https://copartner.in/forgot-password?token={entity.Token}";
           var statusCode = await _eMailFactory.PostEmailAsync(new string[] { entity.Email }, new string[] { }, "Password Reset", $"Reset your password using this link: {resetLink}","Support");
            return statusCode;
        }
    }
}
