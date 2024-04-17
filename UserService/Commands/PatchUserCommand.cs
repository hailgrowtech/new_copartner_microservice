using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using MigrationDB.Models;
using UserService.Dtos;


namespace UserService.Commands;

public record PatchUserCommand(Guid Id, JsonPatchDocument<UserCreateDto> JsonPatchDocument, User User) : IRequest<User>;
