using FeaturesService.Dtos;
using MediatR;
using MigrationDB.Model;

namespace FeaturesService.Queries;


public record GetChatPlanByExpertsIdQuery(Guid Id, int page , int pageSize) : IRequest<IEnumerable<ChatPlan>>;
