using MediatR;

using MigrationDB.Models;
using MigrationDB.Data;
using AffiliatePartnerService.Commands;
using MigrationDB.Model;
using CommonLibrary;
using System.Web;
using AffiliatePartnerService.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;


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

        // Use the first 4 letters of the affiliate partner's name
        string namePart = affiliatePartner.Name.Length >= 4
            ? affiliatePartner.Name.Substring(0, 4).ToUpper()
            : affiliatePartner.Name.ToUpper();

        // Fetch existing links to find the last generated number
        var existingLinks = await _dbContext.APGeneratedLinks
            .Where(link => link.APId == request.AffiliatePartnerId)
            .ToListAsync();

        int startNumber = 1;

        if (existingLinks.Count > 0)
        {
            var regex = new Regex(Regex.Escape(namePart) + @"(\d+)$");
            var lastLink = existingLinks
                .Select(link => new { link.GeneratedLink, Match = regex.Match(link.GeneratedLink) })
                .Where(x => x.Match.Success)
                .OrderByDescending(x => int.Parse(x.Match.Groups[1].Value))
                .FirstOrDefault();

            if (lastLink != null)
            {
                startNumber = int.Parse(lastLink.Match.Groups[1].Value) + 1;
            }
        }

        var links = new List<APGeneratedLinkReadDTO>();

        for (int i = 0; i < request.Num; i++)
        {
            string newLink = $"aplink.copartner.in/{namePart}{startNumber + i}";
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