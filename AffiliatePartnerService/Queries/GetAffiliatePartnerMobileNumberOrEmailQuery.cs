using MediatR;
using MigrationDB.Model;
using MigrationDB.Models;

namespace AffiliatePartnerService.Queries;
public record GetAffiliatePartnerMobileNumberOrEmailQuery(AffiliatePartner AffiliatePartner) : IRequest<AffiliatePartner>;



