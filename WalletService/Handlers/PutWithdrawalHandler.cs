using WalletService.Commands;
using MediatR;
using MigrationDB.Data;
using MigrationDB.Model;

namespace WalletService.Handlers
{
    public class PutWithdrawalHandler : IRequestHandler<PutWithdrawalCommand, Withdrawal>
    {
        private readonly CoPartnerDbContext _dbContext;
        public PutWithdrawalHandler(CoPartnerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Withdrawal> Handle(PutWithdrawalCommand request, CancellationToken cancellationToken)
        {
            var entity = request.Withdrawal;

            var existingEntity = await _dbContext.Withdrawals.FindAsync(entity.Id);
            if (existingEntity == null)
            {
                return null; // or throw an exception indicating the entity not found
            }

            _dbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entity; // Return the updated entity
        }
    }
    public class PutWithdrawalModeHandler : IRequestHandler<PutWithdrawalModeCommand, WithdrawalMode>
    {
        private readonly CoPartnerDbContext _dbContext;
        public PutWithdrawalModeHandler(CoPartnerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<WithdrawalMode> Handle(PutWithdrawalModeCommand request, CancellationToken cancellationToken)
        {
            var entity = request.WithdrawalMode;

            var existingEntity = await _dbContext.WithdrawalModes.FindAsync(entity.Id);
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
