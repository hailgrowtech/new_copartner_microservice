using CommonLibrary.CommonDTOs;
using Microsoft.AspNetCore.JsonPatch;
using AdminDashboardService.Dtos;
using AdminDashboardService.Dtos;

namespace AdminDashboardService.Logic;

public interface IAdAgencyDetailsBusinessProcessor
{
    Task<ResponseDto> Get();
    Task<ResponseDto> Get(Guid id);
    Task<ResponseDto> Post(AdAgencyDetailsCreateDto adagency);
    Task<ResponseDto> Put(Guid id, AdAgencyDetailsCreateDto agencyDetailsCreateDto);
    Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<AdAgencyDetailsCreateDto> adAgencyDto);
    Task<ResponseDto> Delete(Guid id);
    
}
