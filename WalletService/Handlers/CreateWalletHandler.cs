using MediatR;
using MigrationDB.Data;
using MigrationDB.Model;
using WalletService.Commands;


namespace WalletService.Handlers;

public class CreateWalletHandler : IRequestHandler<CreateWalletCommand, Wallet>
{
    private readonly CoPartnerDbContext _dbContext;
    public CreateWalletHandler(CoPartnerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Wallet> Handle(CreateWalletCommand request, CancellationToken cancellationToken)
    {
        var entity = request.Wallet;
        await _dbContext.Wallets.AddAsync(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        request.Wallet.Id = entity.Id;
        return request.Wallet;
    }
}