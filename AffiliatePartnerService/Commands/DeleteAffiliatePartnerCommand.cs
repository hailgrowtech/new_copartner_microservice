using MediatR;
using MigrationDB.Model;
using MigrationDB.Models;

namespace AffiliatePartnerService.Commands
{
    public record DeleteAffiliatePartnerCommand (Guid Id) : IRequest<AffiliatePartner>;

}
