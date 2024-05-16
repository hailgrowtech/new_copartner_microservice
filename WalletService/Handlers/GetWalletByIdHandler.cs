using MediatR;
using Microsoft.EntityFrameworkCore;

using MigrationDB.Models;
using MigrationDB.Data;
using WalletService.Queries;
using MigrationDB.Model;
using System.Drawing.Printing;
using Ocelot.RequestId;
using WalletService.Dtos;
using Azure.Core;

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
        var walletBalance = _dbContext.Wallets
    .Where(w => !w.IsDeleted &&
    (request.userType == "RA" ? w.ExpertsId == request.Id : w.AffiliatePartnerId == request.Id))
        .Sum(w => request.userType == "RA" ? w.RAAmount : w.APAmount);

        var withdrawalBalance = _dbContext.Withdrawals
            .Where(w => !w.IsDeleted && w.WithdrawalBy == request.userType)
            .Sum(w => w.Amount);

        var dto = new WalletWithdrawalReadDto
        {
            Id = request.Id, // Assuming you want to assign the same ID from the request
            WalletBalance = walletBalance,
            WithdrawalBalance = walletBalance - withdrawalBalance
        };

        return dto;
    }

}

