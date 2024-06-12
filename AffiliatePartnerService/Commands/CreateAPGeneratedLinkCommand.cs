using AffiliatePartnerService.Dtos;
using MediatR;
using MigrationDB.Model;
using MigrationDB.Models;

namespace AffiliatePartnerService.Commands;

public record CreateAPGeneratedLinkCommand(Guid AffiliatePartnerId, int Num, string APReferralLink) : IRequest<List<APGeneratedLinkReadDTO>>;

