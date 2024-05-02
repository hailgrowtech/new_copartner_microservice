using CommonLibrary.CommonDTOs;
using Microsoft.AspNetCore.JsonPatch;
using BlogService.Dtos;
using AdvertisingAgencyService.Dtos;

namespace AdvertisingAgencyService.Logic;

public interface IAdAgencyDetailsBusinessProcessor
{
    Task<ResponseDto> Get();
    Task<ResponseDto> Get(Guid id);
    Task<ResponseDto> Post(AdAgencyDetailsCreateDto adagency);
    Task<ResponseDto> Put(Guid id, AdAgencyDetailsCreateDto agencyDetailsCreateDto);
    Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<AdAgencyDetailsCreateDto> adAgencyDto);
    Task<ResponseDto> Delete(Guid id);
    
}
