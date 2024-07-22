using ExpertsService.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;

namespace ExpertsService.Handlers
{
    public class GetStandardQuestionsByExpertIdHandler : IRequestHandler<GetStandardQuestionsByExpertIdQuery, IEnumerable<StandardQuestions>>
    {
        private readonly CoPartnerDbContext _dbContext;
        public GetStandardQuestionsByExpertIdHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<StandardQuestions>> Handle(GetStandardQuestionsByExpertIdQuery request, CancellationToken cancellationToken)
        {
            int skip = (request.Page - 1) * request.PageSize;

            var entities = await _dbContext.StandardQuestions.Where(x => x.IsDeleted != true && x.ExpertId == request.ExpertId)
                            //.OrderByDescending(x => x.CreatedOn)
                            .Skip(skip)
                            .Take(request.PageSize)
                            .ToListAsync(cancellationToken);
            if (entities == null) return null;
            return entities;

        }
    }
}
