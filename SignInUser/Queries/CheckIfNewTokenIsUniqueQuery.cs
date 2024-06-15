using MediatR;

namespace SignInService.Queries;
public record CheckIfNewTokenIsUniqueQuery(string NewToken) : IRequest<bool>;


 
