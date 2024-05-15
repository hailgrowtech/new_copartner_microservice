using MediatR;
using Microsoft.EntityFrameworkCore;

using MigrationDB.Models;
using MigrationDB.Data;
using WalletService.Queries;
using MigrationDB.Model;
using System.Drawing.Printing;
using Ocelot.RequestId;
using WalletService.Dtos;

namespace WalletService.Handlers;

public class GetWalletByIdHandler : IRequestHandler<GetWalletByIdQuery, WalletWithdrawalReadDto>
{
    private readonly CoPartnerDbContext _dbContext;

    public GetWalletByIdHandler(CoPartnerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<WalletWithdrawalReadDto> Handle(GetWalletByIdQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Wallet> query = _dbContext.Wallets.Where(w => !w.IsDeleted);

        if (request.userType == "RA")
        {
            query = query.Where(w => w.ExpertsId == request.Id);
        }
        else
        {
            query = query.Where(w => w.AffiliatePartnerId == request.Id);
        }

        var balances = await query
            .GroupBy(w => 1) // Grouping by a constant to allow aggregate functions
            .Select(g => new
            {
                RABalance = g.Sum(w => w.RAAmount),
                APBalance = g.Sum(w => w.APAmount)
            })
            .FirstOrDefaultAsync();

        var dto = new WalletWithdrawalReadDto
        {
            Id = request.Id, // Assuming you want to assign the same ID from the request
            WalletBalance = balances?.RABalance ?? 0,
            WithdrawalBalance = balances?.APBalance ?? 0
        };

        return dto;
    }

}

