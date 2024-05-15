using MediatR;
using MigrationDB.Data;
using MigrationDB.Model;
using WalletService.Commands;


namespace WalletService.Handlers;

public class CreateWithdrawalHandler : IRequestHandler<CreateWithdrawalCommand, Withdrawal>
{
    private readonly CoPartnerDbContext _dbContext;
    public CreateWithdrawalHandler(CoPartnerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Withdrawal> Handle(CreateWithdrawalCommand request, CancellationToken cancellationToken)
    {
        var entity = request.Withdrawal;
        await _dbContext.Withdrawals.AddAsync(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        request.Withdrawal.Id = entity.Id;
        return request.Withdrawal;
    }
}
public class  CreateWithdrawalModeHandler : IRequestHandler<CreateWithdrawalModeCommand, WithdrawalMode>
{
    private readonly CoPartnerDbContext _dbContext;
    public CreateWithdrawalModeHandler(CoPartnerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<WithdrawalMode> Handle(CreateWithdrawalModeCommand request, CancellationToken cancellationToken)
    {
        var entity = request.WithdrawalMode;
        await _dbContext.WithdrawalModes.AddAsync(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        request.WithdrawalMode.Id = entity.Id;
        return request.WithdrawalMode;
    }
}