using MediatR;
using MigrationDB.Models;
using UserService.Dtos;

namespace UserService.Queries;


public record GetUserByLinkQuery : IRequest<IEnumerable<User>>
{
    public int Page { get; init; }
    public int PageSize { get; init; }
    public string link { get; }

    public GetUserByLinkQuery(int page, int pageSize, string Link)
    {
        Page = page;
        PageSize = pageSize;
        link = Link;
    }
}
