using CommonLibrary.CommonDTOs;
using Microsoft.AspNetCore.JsonPatch;
using AdminDashboardService.Dtos;
using AdminDashboardService.Dtos;

namespace AdminDashboardService.Logic;

public interface IExpertsAdAgencyBusinessProcessor
{
    Task<ResponseDto> Get();
    Task<ResponseDto> Get(Guid id);
    Task<ResponseDto> Post(ExpertsAdAgencyCreateDto expertsAdagency);
    Task<ResponseDto> Put(Guid id, ExpertsAdAgencyCreateDto expertsAgencyCreateDto);
    Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<ExpertsAdAgencyCreateDto> expertsAdAgencyDto);
    Task<ResponseDto> Delete(Guid id);
    
}
