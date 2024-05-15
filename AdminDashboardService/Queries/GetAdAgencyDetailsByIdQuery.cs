using AdminDashboardService.Dtos;
using MediatR;
using MigrationDB.Model;
using MigrationDB.Models;


namespace AdminDashboardService.Queries;
public record GetAdAgencyDetailsByIdQuery(Guid Id) : IRequest<AdAgencyDetailsDto>;


 
