using AffiliatePartnerService.Dtos;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using MigrationDB.Model;

namespace AffiliatePartnerService.Commands;

public record PatchAPGeneratedLinkCommand(Guid Id, JsonPatchDocument<APGeneratedLinkCreateDTO> JsonPatchDocument, APGeneratedLinks apGeneratedLinks) : IRequest<APGeneratedLinks>;
