using CommonLibrary.CommonDTOs;
using AdminDashboardService.Dtos;
using Microsoft.AspNetCore.JsonPatch;

namespace AdminDashboardService.Logic;

public interface IMarketingContentBusinessProcessor
{
    Task<ResponseDto> Get(int page = 1, int pageSize = 10);
    Task<ResponseDto> Get(Guid id);
    Task<ResponseDto> Post(MarketingContentCreateDto marketingContent);
    Task<ResponseDto> Put(Guid id, MarketingContentCreateDto marketingContentCreateDto);
    Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<MarketingContentCreateDto> marketingContentDto);
    Task<ResponseDto> Delete(Guid id);
}
