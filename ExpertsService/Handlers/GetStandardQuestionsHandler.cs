using ExpertService.Queries;
using ExpertsService.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;

namespace ExpertsService.Handlers
{
    public class GetStandardQuestionsHandler : IRequestHandler<GetStandardQuestionsQuery, IEnumerable<StandardQuestions>>
    {
        private readonly CoPartnerDbContext _dbContext;
        public GetStandardQuestionsHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<StandardQuestions>> Handle(GetStandardQuestionsQuery request, CancellationToken cancellationToken)
        {
            int skip = (request.Page - 1) * request.PageSize;

            var entities = await _dbContext.StandardQuestions.Where(x => x.IsDeleted != true)
                            //.OrderByDescending(x => x.CreatedOn)
                            .Skip(skip)
                            .Take(request.PageSize)
                            .ToListAsync(cancellationToken);
            if (entities == null) return null;
            return entities;

        }
    }
}
