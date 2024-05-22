using MediatR;
using MigrationDB.Model;
using MigrationDB.Models;
using WalletService.Dtos;


namespace WalletService.Queries;
public record GetWithdrawalByIdQuery(Guid Id) : IRequest<WithdrawalDetailsReadDto>;
public record GetWithdrawalModeByIdQuery(Guid Id) : IRequest<WithdrawalMode>;


public record GetWithdrawalModeByUserIdQuery(Guid Id, string userType, int page, int pageSize) : IRequest<IEnumerable<WithdrawalMode>>;
public record GetWithdrawalByUserIdQuery(Guid Id, string userType, int page, int pageSize) : IRequest<IEnumerable<WithdrawalDetailsReadDto>>;




