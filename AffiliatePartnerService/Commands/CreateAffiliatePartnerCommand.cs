using MediatR;
using MigrationDB.Model;
using MigrationDB.Models;

namespace AffiliatePartnerService.Commands;

public record CreateAffiliatePartnerCommand(AffiliatePartner AffiliatePartner) : IRequest<AffiliatePartner>;

