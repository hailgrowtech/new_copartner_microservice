using MediatR;

using MigrationDB.Models;
using MigrationDB.Data;
using AffiliatePartnerService.Commands;
using MigrationDB.Model;
using CommonLibrary;
using System.Web;
using AffiliatePartnerService.Dtos;
using Microsoft.EntityFrameworkCore;


namespace AffiliatePartnerService.Handlers;
public class CreateAPGeneratedLinkHandler : IRequestHandler<CreateAPGeneratedLinkCommand, List<APGeneratedLinkReadDTO>>
{
    private readonly CoPartnerDbContext _dbContext;

    public CreateAPGeneratedLinkHandler(CoPartnerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<APGeneratedLinkReadDTO>> Handle(CreateAPGeneratedLinkCommand request, CancellationToken cancellationToken)
    {
        var affiliatePartner = await _dbContext.AffiliatePartners.FindAsync(request.AffiliatePartnerId);
        if (affiliatePartner == null)
        {
            return null;
        }

        string firstNamePart = affiliatePartner.Name.Substring(0, 3).ToUpper();
        string lastNamePart = affiliatePartner.Name.Substring(affiliatePartner.Name.LastIndexOf(" ") + 1, 2).ToUpper();

        // Fetch existing links to find the last generated number
        var existingLinks = await _dbContext.APGeneratedLinks
            .Where(link => link.APId == request.AffiliatePartnerId)
            .ToListAsync();

        int startNumber = existingLinks.Count > 0
            ? existingLinks.Max(link => int.Parse(link.GeneratedLink.Split(new[] { firstNamePart + lastNamePart }, StringSplitOptions.None)[1])) + 1
            : 1;

        var links = new List<APGeneratedLinkReadDTO>();

        for (int i = 0; i < request.Num; i++)
        {
            string newLink = $"aplink.copartner.in/{firstNamePart}{lastNamePart}{startNumber + i}";
            var generatedLink = new APGeneratedLinks
            {
                APId = request.AffiliatePartnerId,  // Ensure this is stored as a Guid
                GeneratedLink = newLink,
                APReferralLink = request.APReferralLink
            };

            await _dbContext.APGeneratedLinks.AddAsync(generatedLink, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            links.Add(new APGeneratedLinkReadDTO
            {
                Id = generatedLink.Id,
                APId = generatedLink.APId,
                GeneratedLink = generatedLink.GeneratedLink,
                APReferralLink = generatedLink.APReferralLink
            });
        }

        return links;
    }
}