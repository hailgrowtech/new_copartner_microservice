using MediatR;

using MigrationDB.Models;
using MigrationDB.Data;
using AffiliatePartnerService.Commands;
using MigrationDB.Model;
using CommonLibrary;


namespace AffiliatePartnerService.Handlers;
public class  CreateAffiliatePartnerHandler : IRequestHandler<CreateAffiliatePartnerCommand, AffiliatePartner>
{
    private readonly CoPartnerDbContext _dbContext;

    public CreateAffiliatePartnerHandler(CoPartnerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AffiliatePartner> Handle(CreateAffiliatePartnerCommand request, CancellationToken cancellationToken)
    {

        var entity = request.AffiliatePartner;
        entity.ReferralCode = RefarralCode.GenerateReferralCode(entity.Name);
        await _dbContext.AffiliatePartners.AddAsync(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        request.AffiliatePartner.Id = entity.Id;
        return request.AffiliatePartner;
    }
}