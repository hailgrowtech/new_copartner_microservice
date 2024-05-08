using MediatR;
using Microsoft.EntityFrameworkCore;

using MigrationDB.Models;
using MigrationDB.Data;
using WalletService.Queries;
using MigrationDB.Model;

namespace WalletService.Handlers;

public class GetWithdrawalByIdHandler : IRequestHandler<GetWithdrawalByIdQuery, Withdrawal>
{
    private readonly CoPartnerDbContext _dbContext;

    public GetWithdrawalByIdHandler(CoPartnerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Withdrawal> Handle(GetWithdrawalByIdQuery request, CancellationToken cancellationToken)
    {
        var withdrawalList = await _dbContext.Withdrawals.Where(a => a.Id == request.Id && a.IsDeleted != true).SingleOrDefaultAsync(cancellationToken: cancellationToken);
        return withdrawalList;
    }
}

public class GetWithdrawalModeByIdHandler : IRequestHandler<GetWithdrawalModeByIdQuery, WithdrawalMode>
{
    private readonly CoPartnerDbContext _dbContext;

    public GetWithdrawalModeByIdHandler(CoPartnerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<WithdrawalMode> Handle(GetWithdrawalModeByIdQuery request, CancellationToken cancellationToken)
    {
        var withdrawalModeList = await _dbContext.WithdrawalModes.Where(a => a.Id == request.Id && a.IsDeleted != true).SingleOrDefaultAsync(cancellationToken: cancellationToken);
        return withdrawalModeList;
    }
}