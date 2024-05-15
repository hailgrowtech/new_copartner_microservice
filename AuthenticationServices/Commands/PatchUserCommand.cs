using AuthenticationService.Models;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using AuthenticationService.Dtos;


namespace AuthenticationService.Commands;

public record PatchUserCommand(Guid Id, JsonPatchDocument<UserCreateDto> JsonPatchDocument, AuthenticationDetail Users) : IRequest<AuthenticationDetail>;
