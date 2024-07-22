using ExpertService.Queries;
using ExpertsService.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;

namespace ExpertsService.Handlers
{
    public class GetStandardQuestionsByIdHandler : IRequestHandler<GetStandardQuestionByIdQuery, StandardQuestions>
    {
        private readonly CoPartnerDbContext _dbContext;

        public GetStandardQuestionsByIdHandler(CoPartnerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<StandardQuestions> Handle(GetStandardQuestionByIdQuery request, CancellationToken cancellationToken)
        {
            var expertsList = await _dbContext.StandardQuestions.Where(a => a.Id == request.Id && a.IsDeleted != true).SingleOrDefaultAsync(cancellationToken: cancellationToken);
            return expertsList;
        }

    }
}
