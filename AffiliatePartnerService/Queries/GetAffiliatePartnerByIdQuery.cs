using MediatR;
using MigrationDB.Model;
using MigrationDB.Models;


namespace AffiliatePartnerService.Queries;
public record GetAffiliatePartnerByIdQuery(Guid Id) : IRequest<AffiliatePartner>;


 
