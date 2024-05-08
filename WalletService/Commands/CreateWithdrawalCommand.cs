using MediatR;
using MigrationDB.Model;
using MigrationDB.Models;

namespace WalletService.Commands;
public record CreateWithdrawalCommand(Withdrawal Withdrawal) : IRequest<Withdrawal>;
public record CreateWithdrawalModeCommand(WithdrawalMode WithdrawalMode) : IRequest<WithdrawalMode>;

