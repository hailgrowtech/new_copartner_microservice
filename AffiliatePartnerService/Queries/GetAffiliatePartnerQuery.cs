using MediatR;
using MigrationDB.Model;
using MigrationDB.Models;


namespace AffiliatePartnerService.Queries;
public record GetAffiliatePartnerQuery : IRequest<IEnumerable<AffiliatePartner>>;


 
