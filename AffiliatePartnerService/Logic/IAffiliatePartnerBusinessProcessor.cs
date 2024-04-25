using AffiliatePartnerService.Dtos;
using CommonLibrary.CommonDTOs;
using Microsoft.AspNetCore.JsonPatch;

namespace AffiliatePartnerService.Logic
{
    public interface IAffiliatePartnerBusinessProcessor
    {
        Task<ResponseDto> Get();
        Task<ResponseDto> Get(Guid id);
        Task<ResponseDto> Post(AffiliatePartnerCreateDTO affiliatePartner);
        Task<ResponseDto> Put(Guid id, AffiliatePartnerCreateDTO affiliatePartnerCreateDTO);
        Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<AffiliatePartnerCreateDTO> affiliatePartnerCreateDTO);
        Task<ResponseDto> Delete(Guid id);


        Task<ResponseDto> GenerateReferralLink(Guid affiliatePartnerId);
    }
}
