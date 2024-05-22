using MediatR;
using MigrationDB.Models;


namespace UserService.Queries;
public record GetUsersQuery : IRequest<IEnumerable<User>>
{
    public int Page { get; init; }
    public int PageSize { get; init; }

    public GetUsersQuery(int page, int pageSize)
    {
        Page = page;
        PageSize = pageSize;
    }
}


 
