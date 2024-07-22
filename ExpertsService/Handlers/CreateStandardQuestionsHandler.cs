using ExpertService.Commands;
using ExpertsService.Commands;
using MediatR;
using MigrationDB.Data;
using MigrationDB.Model;

namespace ExpertsService.Handlers
{
    public class CreateStandardQuestionsHandler : IRequestHandler<CreateStandardQuestionsCommand, StandardQuestions>
    {
        private readonly CoPartnerDbContext _dbContext;
        public CreateStandardQuestionsHandler(CoPartnerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<StandardQuestions> Handle(CreateStandardQuestionsCommand request, CancellationToken cancellationToken)
        {
            var entity = request.StandardQuestions;
            await _dbContext.StandardQuestions.AddAsync(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            request.StandardQuestions.Id = entity.Id;
            //request.Experts.isActive = entity.isActive;
            return request.StandardQuestions;
        }
    }
}
