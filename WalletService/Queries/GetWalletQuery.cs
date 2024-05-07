using MediatR;
using MigrationDB.Model;
using MigrationDB.Models;

namespace WalletService.Queries;
public record GetWalletQuery : IRequest<IEnumerable<Wallet>>
{
    public int Page { get; init; }
    public int PageSize { get; init; }

    public GetWalletQuery(int page, int pageSize)
    {
        Page = page;
        PageSize = pageSize;
    }
}



