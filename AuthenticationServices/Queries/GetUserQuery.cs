using AuthenticationService.Models;
using MediatR;



namespace AuthenticationService.Queries;
public record GetUserQuery : IRequest<IEnumerable<AuthenticationDetail>>
{
    public string UserType { get; set; }
    public int Page { get; init; }
    public int PageSize { get; init; }
    public GetUserQuery(string userType,int page, int pageSize)
    {
        UserType = userType;
        Page = page;
        PageSize = pageSize;
    }
}


