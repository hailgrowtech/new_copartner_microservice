using MassTransit.Courier.Contracts;
using MediatR;
using AuthenticationService.Data;
using AuthenticationService.Models;
using AuthenticationService.Commands;

namespace AuthenticationService.Handlers
{
    public class PutUserHandler : IRequestHandler<PutUserCommand, AuthenticationDetail>
    {
        private readonly AuthenticationDbContext _dbContext;
        public PutUserHandler(AuthenticationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AuthenticationDetail> Handle(PutUserCommand request, CancellationToken cancellationToken)
        {
            var entity = request.Users;

            var existingEntity = await _dbContext.AuthenticationDetails.FindAsync(entity.Id);
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
