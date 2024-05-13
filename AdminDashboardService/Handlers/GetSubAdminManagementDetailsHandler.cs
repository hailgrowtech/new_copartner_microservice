using AdminDashboardService.Dtos;
using AdminDashboardService.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;

namespace AdminDashboardService.Handlers
{
    public class GetSubAdminManagementDetailsHandler : IRequestHandler<GetSubAdminManagementQuery, IEnumerable<SubAdminManagementDto>>
    {
        private readonly CoPartnerDbContext _dbContext;
        public GetSubAdminManagementDetailsHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;


        public async Task<IEnumerable<SubAdminManagementDto>> Handle(GetSubAdminManagementQuery request, CancellationToken cancellationToken)
        {

                
            //// Perform actual database query here
            //var entities = await _dbContext.Credentials
            //    .Where(x => x.IsDeleted != true)
            //    .ToListAsync(cancellationToken);

            //var subAdminManagementDtos = new List<SubAdminManagementDto>();

            //foreach (var entity in entities)
            //{
            //    // Map database entity to DTO
            //    var dto = new SubAdminManagementDto
            //    {
            //        Date = DateTime.Now,
            //        Name = entity.Name, // Example: Mapping Name from entity
            //        Type = entity.Type, // Example: Mapping Type from entity
            //        Status = false, // Example: Setting Status to false
            //    };

            //    subAdminManagementDtos.Add(dto);
            //}





            //if (SubAdminManagementDto == null) return null;
            return null;
        }

    }
}
