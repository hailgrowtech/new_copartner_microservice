using MediatR;
using MigrationDB.Model;
using MigrationDB.Models;

namespace WalletService.Queries;
public record GetWithdrawalQuery : IRequest<IEnumerable<Withdrawal>>;
public record GetWithdrawalModeQuery : IRequest<IEnumerable<WithdrawalMode>>;


 
