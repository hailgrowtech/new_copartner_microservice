using MediatR;
using MigrationDB.Model;
using MigrationDB.Models;


namespace WalletService.Queries;
public record GetWithdrawalByIdQuery(Guid Id) : IRequest<Withdrawal>;
public record GetWithdrawalModeByIdQuery(Guid Id) : IRequest<WithdrawalMode>;


 
