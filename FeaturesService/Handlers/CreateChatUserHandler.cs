using FeaturesService.Commands;
using MediatR;
using MigrationDB.Data;
using MigrationDB.Model;

namespace FeaturesService.Handlers;

public class CreateFreeChatHandler : IRequestHandler<CreateFreeChatCommand, FreeChat>
{
    private readonly CoPartnerDbContext _dbContext;
    public CreateFreeChatHandler(CoPartnerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<FreeChat> Handle(CreateFreeChatCommand request, CancellationToken cancellationToken)
    {
        var entity = request.FreeChat;
        await _dbContext.FreeChats.AddAsync(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        request.FreeChat.Id = entity.Id;
        //request.Experts.isActive = entity.isActive;
        return request.FreeChat;
    }
}
