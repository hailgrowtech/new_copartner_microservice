using CommonLibrary.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Models;
using UserService.Dtos;
using UserService.Queries;

namespace UserService.Handlers
{

    public class GetUserByMobileNumberHandler : IRequestHandler<GetUserByMobileNumberQuery, User>
    {
        private readonly CoPartnerDbContext _dbContext;
        public GetUserByMobileNumberHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;

        public async Task<User> Handle(GetUserByMobileNumberQuery request, CancellationToken cancellationToken)
        {
            // Check if the user already exists
            var existingUser = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.MobileNumber == request.MobileNumber, cancellationToken);

            if (existingUser != null)
            {
                // User exists, return the user
                return existingUser;
            }

            // User does not exist, insert from TempUser
            var tempUser = await _dbContext.TempUsers
                .FirstOrDefaultAsync(tu => tu.MobileNumber == request.MobileNumber, cancellationToken);

            if (tempUser != null)
            {
                // Create a new User instance
                var newUser = new User();

                // Copy all properties from TempUser to User
                foreach (var property in typeof(TempUser).GetProperties())
                {
                    var value = property.GetValue(tempUser);
                    var targetProperty = typeof(User).GetProperty(property.Name);

                    if (targetProperty != null && targetProperty.CanWrite)
                    {
                        targetProperty.SetValue(newUser, value);
                    }
                }

                // Add the new user to the Users table
                _dbContext.Users.Add(newUser);
                await _dbContext.SaveChangesAsync(cancellationToken);

                // Return the newly inserted user
                return newUser;
            }

            // User not found in TempUser either
            return null;
        }

    }
}
