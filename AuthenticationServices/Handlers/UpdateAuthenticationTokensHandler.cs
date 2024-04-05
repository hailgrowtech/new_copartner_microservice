using MediatR;
using AuthenticationService.Data;
using AuthenticationService.Models;
using AuthenticationService.Commands;

namespace AuthenticationService.Handlers;
public class UpdateAuthenticationTokensHandler : IRequestHandler<UpdateAuthenticationTokensCommand, Authentication>
{
    private readonly AuthenticationDbContext _dbContext;
    public UpdateAuthenticationTokensHandler(AuthenticationDbContext dbContext) => _dbContext = dbContext;
    public async Task<Authentication> Handle(UpdateAuthenticationTokensCommand request, CancellationToken cancellationToken)
    {
        var entity = request.Authentication;
        request.refreshToken.Id = Guid.NewGuid();
        if (entity == null) return null;

        // replace old refresh token with a new one (rotate token)
        entity.RefreshTokens.Add(request.refreshToken);

        // remove old refresh tokens from user// Todo should we change the sequence for adding and removing these tokens.
        entity.RefreshTokens.RemoveAll(x => !x.IsActive && x.Created.AddDays(request.RefreshTokenTTL) <= DateTime.UtcNow);

        

        _dbContext.Update(entity);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return await Task.FromResult(entity);
    }
}