using MediatR;
using MigrationDB.Model;
using MigrationDB.Models;
using WalletService.Dtos;


namespace WalletService.Queries;
public record GetWithdrawalByIdQuery(Guid Id) : IRequest<WithdrawalDetailsReadDto>;
public record GetWithdrawalModeByIdQuery(Guid Id) : IRequest<WithdrawalMode>;


public record GetWithdrawalModeByUserIdQuery(Guid Id, string userType) : IRequest<IEnumerable<WithdrawalMode>>
{
    public int Page { get; init; }
    public int PageSize { get; init; }
    public string RequestBy { get; set; }

    public GetWithdrawalModeByUserIdQuery(int page, int pageSize, string userType)
    {
        Page = page;
        PageSize = pageSize;
        RequestBy = userType;
    }
}



