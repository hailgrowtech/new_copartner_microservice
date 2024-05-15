using AdminDashboardService.Dtos;
using AdminDashboardService.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;

namespace AdminDashboardService.Handlers
{
    //public class GetSubAdminManagementDetailsByIdHandler :  IRequestHandler<GetSubAdminManagementByIdQuery, IEnumerable<SubAdminManagementReadDto>>
    //{
    //    private readonly CoPartnerDbContext _dbContext;

    //    public GetSubAdminManagementDetailsByIdHandler(CoPartnerDbContext dbContext)
    //    {
    //        _dbContext = dbContext;
    //    }

    //    public async Task<SubAdminManagementReadDto> Handle(GetSubAdminManagementByIdQuery request, CancellationToken cancellationToken)
    //    {
    //        //var RelationshipManagersList = await _dbContext.RelationshipManagers.Where(a => a.Id == request.Id && a.IsDeleted != true).SingleOrDefaultAsync(cancellationToken: cancellationToken);
    //        return null;
    //    }
    //}
};
