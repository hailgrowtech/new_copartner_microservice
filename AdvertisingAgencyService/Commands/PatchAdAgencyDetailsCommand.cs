using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using BlogService.Dtos;
using MigrationDB.Model;
using AdvertisingAgencyService.Dtos;


namespace AdvertisingAgencyService.Commands;

public record PatchAdAgencyDetailsCommand(Guid Id, JsonPatchDocument<AdAgencyDetailsCreateDto> JsonPatchDocument, AdvertisingAgency AdvertisingAgency) : IRequest<AdvertisingAgency>;
