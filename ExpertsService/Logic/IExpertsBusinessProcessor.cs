using CommonLibrary.CommonDTOs;
using Microsoft.AspNetCore.JsonPatch;
using ExpertService.Dtos;

namespace ExpertService.Logic;

public interface IExpertsBusinessProcessor
{
    Task<ResponseDto> Get();
    Task<ResponseDto> Get(Guid id);
    Task<ResponseDto> Post(ExpertsCreateDto experts);
    Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<ExpertsCreateDto> expertsDto);
    bool ResetPassword(ExpertsPasswordDTO expertsPasswordDTO);
}
