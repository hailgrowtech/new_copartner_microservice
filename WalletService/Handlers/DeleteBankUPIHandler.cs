using MediatR;
using Microsoft.EntityFrameworkCore;
using MigrationDB.Data;
using MigrationDB.Model;
using MigrationDB.Models;
using WalletService.Commands;

namespace WalletService.Handlers;
public class DeleteBankUPIHandler : IRequestHandler<DeleteBankUPICommand, WithdrawalMode>
{
    private readonly CoPartnerDbContext _dbContext;
    public DeleteBankUPIHandler(CoPartnerDbContext dbContext) => _dbContext = dbContext;

    public async Task<WithdrawalMode> Handle(DeleteBankUPICommand request, CancellationToken cancellationToken)
    {
        var withdrawalMode = await _dbContext.WithdrawalModes.FindAsync(request.Id);
        if (withdrawalMode == null) return null; // or throw an exception indicating the entity not found

        withdrawalMode.IsDeleted = true; // Mark the entity as deleted
        withdrawalMode.DeletedOn = DateTime.UtcNow; // Set the deletion timestamp if needed
        withdrawalMode.DeletedBy = new Guid(); // Set the user who performed the deletion if needed

        await _dbContext.SaveChangesAsync(cancellationToken);
        return withdrawalMode; // Return the deleted entity
    }
}
