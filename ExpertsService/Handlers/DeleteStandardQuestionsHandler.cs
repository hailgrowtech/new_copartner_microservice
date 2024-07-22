﻿using ExpertService.Commands;
using ExpertsService.Commands;
using MediatR;
using MigrationDB.Data;
using MigrationDB.Model;

namespace ExpertsService.Handlers
{
    public class DeleteStandardQuestionsHandler : IRequestHandler<DeleteStandardQuestionsCommand, StandardQuestions>
    {
        private readonly CoPartnerDbContext _dbContext;
        public DeleteStandardQuestionsHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;

        public async Task<StandardQuestions> Handle(DeleteStandardQuestionsCommand request, CancellationToken cancellationToken)
        {
            var experts = await _dbContext.StandardQuestions.FindAsync(request.Id);
            if (experts == null) return null; // or throw an exception indicating the entity not found

            experts.IsDeleted = true; // Mark the entity as deleted
            experts.DeletedOn = DateTime.UtcNow; // Set the deletion timestamp if needed
            experts.DeletedBy = new Guid(); // Set the user who performed the deletion if needed

            await _dbContext.SaveChangesAsync(cancellationToken);
            return experts; // Return the deleted entity
        }
    }
}
