using AffiliatePartnerService.Dtos;
using CommonLibrary.CommonDTOs;
using Microsoft.AspNetCore.JsonPatch;

namespace AffiliatePartnerService.Logic
{
    public interface IAPDetailsBusinessProcessor
    {

        Task<ResponseDto> Get(int page = 1, int pageSize = 10);
        Task<ResponseDto> GenerateAPLink(APGenerateLinkRequestDto requestDto);
        Task<ResponseDto> PatchAPGeneratedLink(Guid Id, JsonPatchDocument<APGeneratedLinkCreateDTO> apGeneratedLinkCreateDTO);

        Task<ResponseDto> GetGeneratedAPLinkById(Guid id);
    }
}
