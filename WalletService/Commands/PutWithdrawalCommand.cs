
using MediatR;
using MigrationDB.Model;
using MigrationDB.Models;

namespace WalletService.Commands
{
    public record PutWithdrawalCommand(Withdrawal Withdrawal) : IRequest<Withdrawal>;
    public record PutWithdrawalModeCommand(WithdrawalMode WithdrawalMode) : IRequest<WithdrawalMode>;
}
