using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using WalletService.Queries;
using MigrationDB.Model;
using System.Drawing.Printing;
using WalletService.Dtos;

namespace WalletService.Handlers;
public class GetWithdrawalHandler : IRequestHandler<GetWithdrawalQuery, IEnumerable<WithdrawalReadDto>>
{
    private readonly CoPartnerDbContext _dbContext;
    public GetWithdrawalHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;
    public async Task<IEnumerable<WithdrawalReadDto>> Handle(GetWithdrawalQuery request, CancellationToken cancellationToken)
    {
        IQueryable<WithdrawalReadDto> query = Enumerable.Empty<WithdrawalReadDto>().AsQueryable();

        if (request.RequestBy == "RA")
        {
            query = _dbContext.Withdrawals
                .Where(w => w.WithdrawalBy == "RA")
                .Join(
                    _dbContext.WithdrawalModes,
                    withdrawal => withdrawal.WithdrawalModeId,
                    withdrawalMode => withdrawalMode.Id,
                    (withdrawal, withdrawalMode) => new { Withdrawal = withdrawal, WithdrawalMode = withdrawalMode })
                .Join(
                    _dbContext.Experts,
                    combined => combined.WithdrawalMode.ExpertsId,
                    expert => expert.Id,
                    (combined, expert) => new WithdrawalReadDto
                    {
                        Id = combined.Withdrawal.Id,
                        Amount = combined.Withdrawal.Amount,
                        WithdrawalModeId = combined.Withdrawal.WithdrawalModeId,
                        WithdrawalRequestDate = combined.Withdrawal.WithdrawalRequestDate,
                        RequestAction = combined.Withdrawal.RequestAction,
                        TransactionId = combined.Withdrawal.TransactionId,
                        TransactionDate = combined.Withdrawal.TransactionDate,
                        RejectReason = combined.Withdrawal.RejectReason,
                        Name = expert.Name,
                        SEBINo = expert.SEBIRegNo
                    });
        }

        if (request.RequestBy == "AP")
        {
            query = _dbContext.Withdrawals
                .Where(w => w.WithdrawalBy == "AP")
                .Join(
                    _dbContext.WithdrawalModes,
                    withdrawal => withdrawal.WithdrawalModeId,
                    withdrawalMode => withdrawalMode.Id,
                    (withdrawal, withdrawalMode) => new { Withdrawal = withdrawal, WithdrawalMode = withdrawalMode })
                .Join(
                    _dbContext.AffiliatePartners,
                    combined => combined.WithdrawalMode.AffiliatePartnerId,
                    affiliatePartner => affiliatePartner.Id,
                    (combined, affiliatePartner) => new WithdrawalReadDto
                    {
                        Id = combined.Withdrawal.Id,
                        Amount = combined.Withdrawal.Amount,
                        WithdrawalModeId = combined.Withdrawal.WithdrawalModeId,
                        WithdrawalRequestDate = combined.Withdrawal.WithdrawalRequestDate,
                        RequestAction = combined.Withdrawal.RequestAction,
                        TransactionId = combined.Withdrawal.TransactionId,
                        TransactionDate = combined.Withdrawal.TransactionDate,
                        RejectReason = combined.Withdrawal.RejectReason,
                        Name = affiliatePartner.Name,
                        MobileNo = affiliatePartner.MobileNumber
                    });
        }

        var results = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

        return results;
    }

}
public class GetWithdrawalModeHandler : IRequestHandler<GetWithdrawalModeQuery, IEnumerable<WithdrawalMode>>
{
    private readonly CoPartnerDbContext _dbContext;
    public GetWithdrawalModeHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;


    public async Task<IEnumerable<WithdrawalMode>> Handle(GetWithdrawalModeQuery request, CancellationToken cancellationToken)
    {
        var entities =  await _dbContext.WithdrawalModes.Where(x => x.IsDeleted != true).ToListAsync(cancellationToken: cancellationToken);
        if (entities == null) return null; 
        return entities;      
    }
}