using CommonLibrary.CommonDTOs;
using Microsoft.AspNetCore.JsonPatch;
using ExpertService.Dtos;

namespace ExpertService.Logic;

public interface IExpertsBusinessProcessor
{
    Task<ResponseDto> Get();
    Task<ResponseDto> Get(Guid id);
    Task<ResponseDto> Post(ExpertsCreateDto experts);
    Task<ResponseDto> Put(Guid id, ExpertsCreateDto expertsCreateDto);
    Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<ExpertsCreateDto> expertsDto);
    Task<ResponseDto> Delete(Guid id);
    Task<ResponseDto> GetListing();
    Task<ResponseDto> GetListingDetails();
    bool ResetPassword(ExpertsPasswordDTO expertsPasswordDTO);


    Task<ResponseDto> GenerateReferralLink(Guid expertId);

}
