using MediatR;
using MigrationDB.Model;

namespace FeaturesService.Commands;

public record CreateFreeChatCommand(FreeChat FreeChat) : IRequest<FreeChat>;