using MediatR;
using Microsoft.EntityFrameworkCore;

using MigrationDB.Models;
using MigrationDB.Data;
using WalletService.Queries;
using MigrationDB.Model;
using WalletService.Dtos;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace WalletService.Handlers;

public class GetWithdrawalByIdHandler : IRequestHandler<GetWithdrawalByIdQuery, WithdrawalDetailsReadDto>
{
    private readonly CoPartnerDbContext _dbContext;

    public GetWithdrawalByIdHandler(CoPartnerDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<WithdrawalDetailsReadDto> Handle(GetWithdrawalByIdQuery request, CancellationToken cancellationToken)
    {
        var requestBy = await (from withdrawal in _dbContext.Withdrawals
                               join withdrawalMode in _dbContext.WithdrawalModes
                               on withdrawal.WithdrawalModeId equals withdrawalMode.Id
                               where withdrawal.Id == request.Id
                               select withdrawalMode.AffiliatePartnerId != null ? "AP" : "RA")
                             .FirstOrDefaultAsync();
        if (requestBy == null)
            return null;
        IQueryable<WithdrawalDetailsReadDto> query = Enumerable.Empty<WithdrawalDetailsReadDto>().AsQueryable();
        if (requestBy == "RA")
        {
             query = _dbContext.Withdrawals
            .Where(w => w.WithdrawalBy == "RA" && w.Id== request.Id)
            .Join(
                _dbContext.WithdrawalModes,
                withdrawal => withdrawal.WithdrawalModeId,
                withdrawalMode => withdrawalMode.Id,
                (withdrawal, withdrawalMode) => new { Withdrawal = withdrawal, WithdrawalMode = withdrawalMode })
            .Join(
                _dbContext.Experts,
                combined => combined.WithdrawalMode.ExpertsId,
                expert => expert.Id,
                (combined, expert) => new WithdrawalDetailsReadDto
                {
                    Id = combined.Withdrawal.Id,
                    Amount = combined.Withdrawal.Amount,
                    Name = expert.Name,
                    AccountHolderName = combined.WithdrawalMode.AccountHolderName,
                    AccountNumber = combined.WithdrawalMode.AccountNumber,
                    IFSCCode = combined.WithdrawalMode.IFSCCode,
                    BankName = combined.WithdrawalMode.BankName,
                    UPI_ID = combined.WithdrawalMode.UPI_ID,
                    RequestAction = combined.Withdrawal.RequestAction
                });
        }

       else if (requestBy == "AP")
        {
            query = _dbContext.Withdrawals
                .Where(w => w.WithdrawalBy == "AP" && w.Id == request.Id)
                .Join(
                    _dbContext.WithdrawalModes,
                    withdrawal => withdrawal.WithdrawalModeId,
                    withdrawalMode => withdrawalMode.Id,
                    (withdrawal, withdrawalMode) => new { Withdrawal = withdrawal, WithdrawalMode = withdrawalMode })
                .Join(
                    _dbContext.AffiliatePartners,
                    combined => combined.WithdrawalMode.AffiliatePartnerId,
                    affiliatePartner => affiliatePartner.Id,
                    (combined, affiliatePartner) => new WithdrawalDetailsReadDto
                    {
                        Id = combined.Withdrawal.Id,
                        Amount = combined.Withdrawal.Amount,
                        Name = affiliatePartner.Name,
                        AccountHolderName = combined.WithdrawalMode.AccountHolderName,
                        AccountNumber = combined.WithdrawalMode.AccountNumber,
                        IFSCCode = combined.WithdrawalMode.IFSCCode,
                        BankName = combined.WithdrawalMode.BankName,
                        UPI_ID = combined.WithdrawalMode.UPI_ID
                    });
        }

        var result = await query.FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (result == null) return null;
        return result;
    }

}

public class GetWithdrawalModeByIdHandler : IRequestHandler<GetWithdrawalModeByIdQuery, WithdrawalMode>
{
    private readonly CoPartnerDbContext _dbContext;

    public GetWithdrawalModeByIdHandler(CoPartnerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<WithdrawalMode> Handle(GetWithdrawalModeByIdQuery request, CancellationToken cancellationToken)
    {
        var withdrawalModeList = await _dbContext.WithdrawalModes.Where(a => a.Id == request.Id && a.IsDeleted != true).SingleOrDefaultAsync(cancellationToken: cancellationToken);
        return withdrawalModeList;
    }
}


public class GetWithdrawalModeByUserIdHandler : IRequestHandler<GetWithdrawalModeByUserIdQuery, IEnumerable<WithdrawalMode>>
{
    private readonly CoPartnerDbContext _dbContext;

    public GetWithdrawalModeByUserIdHandler(CoPartnerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<WithdrawalMode>> Handle(GetWithdrawalModeByUserIdQuery request, CancellationToken cancellationToken)
    {
        IQueryable<WithdrawalMode> query = null;

        if (request.userType == "AP")
        {
            query = _dbContext.WithdrawalModes.Where(a => a.AffiliatePartnerId == request.Id && a.IsDeleted != true);
        }
        else if (request.userType == "RA")
        {
            query = _dbContext.WithdrawalModes.Where(a => a.ExpertsId == request.Id && a.IsDeleted != true);
        }

        if (query != null)
        {
            var results = await query
                .Skip((request.page - 1) * request.pageSize)
                .Take(request.pageSize)
                .ToListAsync(cancellationToken);

            return results;
        }

        return Enumerable.Empty<WithdrawalMode>();

    }
}


public class GetWithdrawalByUserIdHandler : IRequestHandler<GetWithdrawalByUserIdQuery, IEnumerable<WithdrawalReadDto>>
{
    private readonly CoPartnerDbContext _dbContext;
    public GetWithdrawalByUserIdHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;


    public async Task<IEnumerable<WithdrawalReadDto>> Handle(GetWithdrawalByUserIdQuery request, CancellationToken cancellationToken)
    {

        IQueryable<WithdrawalReadDto> query = Enumerable.Empty<WithdrawalReadDto>().AsQueryable();

        if (request.userType == "RA")
        {
            query = _dbContext.Withdrawals
                .Where(w => w.WithdrawalBy == "RA")
                .Join(
                    _dbContext.WithdrawalModes,
                    withdrawal => withdrawal.WithdrawalModeId,
                    withdrawalMode => withdrawalMode.Id,
                    (withdrawal, withdrawalMode) => new { Withdrawal = withdrawal, WithdrawalMode = withdrawalMode })
                .Where(w => w.WithdrawalMode.ExpertsId == request.Id)
                .Join(
                    _dbContext.Experts,
                    combined => combined.WithdrawalMode.ExpertsId,
                    expert => expert.Id,
                    (combined, expert) => new WithdrawalReadDto
                    {
                        Id = combined.Withdrawal.Id,
                        WithdrawalBy = combined.Withdrawal.WithdrawalBy,
                        Amount = combined.Withdrawal.Amount,
                        WithdrawalModeId = combined.Withdrawal.WithdrawalModeId,
                        WithdrawalRequestDate = combined.Withdrawal.WithdrawalRequestDate,
                        RequestAction = combined.Withdrawal.RequestAction,
                        TransactionId = combined.Withdrawal.TransactionId,
                        TransactionDate = combined.Withdrawal.TransactionDate,
                        RejectReason = combined.Withdrawal.RejectReason,
                        Name = expert.Name,
                        SEBINo = expert.SEBIRegNo,
                        MobileNo = expert.MobileNumber,
                        LegalName = expert.LegalName,
                        GST = expert.GST
                        
                    });
        }

        if (request.userType == "AP")
        {
            query = _dbContext.Withdrawals
                .Where(w => w.WithdrawalBy == "AP")
                .Join(
                    _dbContext.WithdrawalModes,
                    withdrawal => withdrawal.WithdrawalModeId,
                    withdrawalMode => withdrawalMode.Id,
                    (withdrawal, withdrawalMode) => new { Withdrawal = withdrawal, WithdrawalMode = withdrawalMode })
                .Where(w => w.WithdrawalMode.AffiliatePartnerId  == request.Id)
                .Join(
                    _dbContext.AffiliatePartners,
                    combined => combined.WithdrawalMode.AffiliatePartnerId,
                    affiliatePartner => affiliatePartner.Id,
                    (combined, affiliatePartner) => new WithdrawalReadDto
                    {
                        Id = combined.Withdrawal.Id,
                        WithdrawalBy = combined.Withdrawal.WithdrawalBy,
                        Amount = combined.Withdrawal.Amount,
                        WithdrawalModeId = combined.Withdrawal.WithdrawalModeId,
                        WithdrawalRequestDate = combined.Withdrawal.WithdrawalRequestDate,
                        RequestAction = combined.Withdrawal.RequestAction,
                        TransactionId = combined.Withdrawal.TransactionId,
                        TransactionDate = combined.Withdrawal.TransactionDate,
                        RejectReason = combined.Withdrawal.RejectReason,
                        Name = affiliatePartner.Name,
                        MobileNo = affiliatePartner.MobileNumber,
                        LegalName = affiliatePartner.LegalName,
                        GST = affiliatePartner.GST

                    });
        }

        var result = await query.ToListAsync(cancellationToken: cancellationToken);

        if (result == null) return null;
        return result;
    }
   }