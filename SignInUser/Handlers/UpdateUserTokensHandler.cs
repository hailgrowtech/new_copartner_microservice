using MediatR;
using SignInService.Data;
using SignInService.Models;
using SignInService.Commands;

namespace SignInService.Handlers;
public class UpdateUserTokensHandler : IRequestHandler<UpdateUserTokensCommand, PotentialCustomer>
{
    private readonly SignInDbContext _dbContext;
    public UpdateUserTokensHandler(SignInDbContext dbContext) => _dbContext = dbContext;
    public async Task<PotentialCustomer> Handle(UpdateUserTokensCommand request, CancellationToken cancellationToken)
    {
        var entity = request.Lead;
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