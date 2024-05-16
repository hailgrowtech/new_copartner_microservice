using MassTransit.Courier.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Models;
using UserService.Commands;

namespace UserService.Handlers
{
    public class PutUserHandler : IRequestHandler<PutUserCommand, User>
    {
        private readonly CoPartnerDbContext _dbContext;
        public PutUserHandler(CoPartnerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> Handle(PutUserCommand request, CancellationToken cancellationToken)
        {
            var entity = request.user;
            User existingEntity;

            if (string.IsNullOrEmpty(request.MobileNo))
            {
                existingEntity = await _dbContext.Users.FindAsync(entity.Id);
            }
            else
            {
                existingEntity = await _dbContext.Users.FirstOrDefaultAsync(u => u.MobileNumber == request.MobileNo);
                if(existingEntity != null) entity.Id= existingEntity.Id;
            }

            if (existingEntity == null)
            {
                return null; // or throw an exception indicating the entity not found
            }
            

            _dbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entity; // Return the updated entity
        }

    }
}
