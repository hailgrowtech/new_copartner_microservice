using CommonLibrary.CommonDTOs;
using Microsoft.AspNetCore.JsonPatch;
using AdminDashboardService.Dtos;
using AdminDashboardService.Dtos;

namespace AdminDashboardService.Logic;

public interface IAdAgencyDetailsBusinessProcessor
{
    Task<ResponseDto> Get(int page = 1, int pageSize = 10);
    Task<ResponseDto> Get(Guid id);
    Task<ResponseDto> Post(AdAgencyDetailsCreateDto adagency);
    Task<ResponseDto> Put(Guid id, AdAgencyDetailsCreateDto agencyDetailsCreateDto);
    Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<AdAgencyDetailsCreateDto> adAgencyDto);
    Task<ResponseDto> Delete(Guid id);
    
}
