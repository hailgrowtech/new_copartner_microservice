using MediatR;
using MigrationDB.Model;

namespace AdminDashboardService.Queries;

public record GetTelegramMessageQuery : IRequest<IEnumerable<TelegramMessage>>
{
    public int Page { get; init; }
    public int PageSize { get; init; }

    public GetTelegramMessageQuery(int page, int pageSize)
    {
        Page = page;
        PageSize = pageSize;
    }
}
