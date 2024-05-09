using MediatR;
using MigrationDB.Model;
using MigrationDB.Models;
using WalletService.Dtos;

namespace WalletService.Queries;
public record GetWithdrawalQuery : IRequest<IEnumerable<WithdrawalReadDto>>
{
    public int Page { get; init; }
    public int PageSize { get; init; }
    public string RequestBy { get; set; }

    public GetWithdrawalQuery(int page, int pageSize, string requestBy)
    {
        Page = page;
        PageSize = pageSize;
        RequestBy = requestBy;
    }
}
public record GetWithdrawalModeQuery : IRequest<IEnumerable<WithdrawalMode>>;


 
