using CommonLibrary.CommonDTOs;
using MarketingContentService.Dtos;
using Microsoft.AspNetCore.JsonPatch;

namespace MarketingContentService.Logic;

public interface IMarketingContentBusinessProcessor
{
    Task<ResponseDto> Get();
    Task<ResponseDto> Get(Guid id);
    Task<ResponseDto> Post(MarketingContentCreateDto marketingContent);
    Task<ResponseDto> Put(Guid id, MarketingContentCreateDto marketingContentCreateDto);
    Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<MarketingContentCreateDto> marketingContentDto);
    Task<ResponseDto> Delete(Guid id);
}
