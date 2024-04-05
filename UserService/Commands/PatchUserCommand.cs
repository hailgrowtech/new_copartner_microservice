using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using UserService.Dtos;
using UserService.Models;

namespace UserService.Commands;

public record PatchUserCommand(Guid Id, JsonPatchDocument<UserCreateDto> JsonPatchDocument, User User) : IRequest<User>;
