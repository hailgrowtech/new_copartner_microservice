using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using WalletService.Queries;
using MigrationDB.Model;

namespace WalletService.Handlers;
public class GetWithdrawalHandler : IRequestHandler<GetWithdrawalQuery, IEnumerable<Withdrawal>>
{
    private readonly CoPartnerDbContext _dbContext;
    public GetWithdrawalHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;


    public async Task<IEnumerable<Withdrawal>> Handle(GetWithdrawalQuery request, CancellationToken cancellationToken)
    {
        var entities = await _dbContext.Withdrawals.Where(x => x.IsDeleted != true).ToListAsync(cancellationToken: cancellationToken);
        if (entities == null) return null;
        return entities;
    }
}
public class GetWithdrawalModeHandler : IRequestHandler<GetWithdrawalModeQuery, IEnumerable<WithdrawalMode>>
{
    private readonly CoPartnerDbContext _dbContext;
    public GetWithdrawalModeHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;


    public async Task<IEnumerable<WithdrawalMode>> Handle(GetWithdrawalModeQuery request, CancellationToken cancellationToken)
    {
        var entities =  await _dbContext.WithdrawalModes.Where(x => x.IsDeleted != true).ToListAsync(cancellationToken: cancellationToken);
        if (entities == null) return null; 
        return entities;      
    }
}