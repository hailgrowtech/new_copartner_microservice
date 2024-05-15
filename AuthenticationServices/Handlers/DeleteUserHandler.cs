using AuthenticationService.Data;
using AuthenticationService.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AuthenticationService.Commands;



namespace AuthenticationService.Handlers;
public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, AuthenticationDetail>
{
    private readonly AuthenticationDbContext _dbContext;
    public DeleteUserHandler(AuthenticationDbContext dbContext) => _dbContext = dbContext;


    public async Task<AuthenticationDetail> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var stackholderList = await _dbContext.AuthenticationDetails.Where(a => a.Id == request.Id).SingleOrDefaultAsync(cancellationToken: cancellationToken);
        if (stackholderList == null) return null;
        _dbContext.AuthenticationDetails.Remove(stackholderList);
        await _dbContext.SaveChangesAsync();
        return stackholderList;
    }
}
