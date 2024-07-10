using CommonLibrary.CommonDTOs;
using FeaturesService.Dtos;
using FeaturesService.Dtos;
using Microsoft.AspNetCore.JsonPatch;

namespace FeaturesService.Logic
{
    public interface IWebinarMstBusinessProcessor
    {
        Task<ResponseDto> Get(int page = 1, int pageSize = 10);
        Task<ResponseDto> Get(Guid id);
        Task<ResponseDto> Post(WebinarMstCreateDto webinarMst);
        Task<ResponseDto> Put(Guid id, WebinarMstCreateDto webinarMstCreateDto);
        Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<WebinarMstCreateDto> webinarMstDto);
        Task<ResponseDto> Delete(Guid id);

    }
}
