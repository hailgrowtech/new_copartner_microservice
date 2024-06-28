using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using WalletService.Queries;
using MigrationDB.Model;
using System.Drawing.Printing;

namespace WalletService.Handlers;
public class GetWalletHandler : IRequestHandler<GetWalletQuery, IEnumerable<Wallet>>
{
    private readonly CoPartnerDbContext _dbContext;
    public GetWalletHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;
    public async Task<IEnumerable<Wallet>> Handle(GetWalletQuery request, CancellationToken cancellationToken)
    {
        // Calculate the number of records to skip
        int skip = (request.Page - 1) * request.PageSize;

        // Retrieve the page of wallets
        var entities = await _dbContext.Wallets
            .Where(x => x.IsDeleted != true)
            .OrderByDescending(x => x.CreatedOn)
            .Skip(skip)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);
        if (entities == null) return null;
        return entities;
    }
}
