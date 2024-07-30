using FeaturesService.Commands;
using MediatR;
using MigrationDB.Data;
using MigrationDB.Model;

namespace FeaturesService.Handlers;

public class CreateChatUserHandler : IRequestHandler<CreateChatUserCommand, ChatUser>
{
    private readonly CoPartnerDbContext _dbContext;
    public CreateChatUserHandler(CoPartnerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ChatUser> Handle(CreateChatUserCommand request, CancellationToken cancellationToken)
    {
        var entity = request.ChatUser;
        await _dbContext.ChatUsers.AddAsync(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        request.ChatUser.Id = entity.Id;
        //request.Experts.isActive = entity.isActive;
        return request.ChatUser;
    }
}
