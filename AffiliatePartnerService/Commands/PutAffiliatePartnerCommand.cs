
using MediatR;
using MigrationDB.Model;
using MigrationDB.Models;

namespace AffiliatePartnerService.Commands
{
    public record PutAffiliatePartnerCommand(AffiliatePartner AffiliatePartner) : IRequest<AffiliatePartner>;
}
