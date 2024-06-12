using MediatR;
using UserService.Dtos;

namespace UserService.Queries
{
    public class GetUserByLinkQuery : IRequest<IEnumerable<UserReadDto>>
    {
        public string link { get; }

        public GetUserByLinkQuery(string Link)
        {
            link = Link;
        }
    }
}


