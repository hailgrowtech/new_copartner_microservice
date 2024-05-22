using CommonLibrary.CommonDTOs;
using Microsoft.AspNetCore.JsonPatch;
using ExpertService.Dtos;

namespace ExpertService.Logic;

public interface IExpertsBusinessProcessor
{
    Task<ResponseDto> Get(int page = 1, int pageSize = 10);
    Task<ResponseDto> Get(Guid id);
    Task<ResponseDto> Post(ExpertsCreateDto experts);
    Task<ResponseDto> Put(Guid id, ExpertsCreateDto expertsCreateDto);
    Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<ExpertsCreateDto> expertsDto);
    Task<ResponseDto> Delete(Guid id);
    Task<ResponseDto> GetListing(int page = 1, int pageSize = 10);
    Task<ResponseDto> GetListingDetails(int page = 1, int pageSize = 10);
    bool ResetPassword(ExpertsPasswordDTO expertsPasswordDTO);


    Task<ResponseDto> GenerateReferralLink(Guid expertId);
    Task<ResponseDto> GenerateExpertPaymentLink(Guid expertId);

}
