using AffiliatePartnerService.Commands;
using AffiliatePartnerService.Dtos;
using AffiliatePartnerService.Queries;
using AutoMapper;
using CommonLibrary;
using CommonLibrary.CommonDTOs;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using MigrationDB.Model;
using System.Web;

namespace AffiliatePartnerService.Logic
{
    public class AffiliatePartnerBusinessProcessor : IAffiliatePartnerBusinessProcessor
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public AffiliatePartnerBusinessProcessor(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        public async Task<ResponseDto> Delete(Guid id)
        {
            var affiliatePartner = await _sender.Send(new DeleteAffiliatePartnerCommand(id));
            var affiliatePartnerReadDto = _mapper.Map<ResponseDto>(affiliatePartner);
            return affiliatePartnerReadDto;
        }

        public async Task<ResponseDto> GenerateReferralLink(Guid id)
        {

            // Assuming _sender.Send is an asynchronous method that returns an AffiliatePartner object
            var ap = await _sender.Send(new GetAffiliatePartnerByIdQuery(id));

            // It's a good practice to use UriBuilder for constructing URLs to handle edge cases
            var uriBuilder = new UriBuilder("https://copartner.in/signup");
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["referralCode"] = ap.ReferralCode;
            query["apid"] = id.ToString(); // Ensure the ID is converted to a string
            uriBuilder.Query = query.ToString();

            var referralLink = uriBuilder.ToString();

            return new ResponseDto()
            {
                IsSuccess = true,
                Data = referralLink,
            };


        }

        public async Task<ResponseDto> Get(int page, int pageSize)
        {
            var affiliatePartnersList = await _sender.Send(new GetAffiliatePartnerQuery(page, pageSize));
            var affiliatePartnersReadDtoList = _mapper.Map<List<AffiliatePartnerReadDTO>>(affiliatePartnersList);
            return new ResponseDto()
            {
                IsSuccess = true,
                Data = affiliatePartnersReadDtoList,
            };
        }

        public async Task<ResponseDto> Get(Guid id)
        {
            var affiliatePartners = await _sender.Send(new GetAffiliatePartnerByIdQuery(id));
            if (affiliatePartners == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = null,
                    ErrorMessages = new List<string>() { AppConstants.AffiliatePartner_AffiliatePartnerNotFound }
                };
            }
            var affiliatePartnersReadDto = _mapper.Map<AffiliatePartnerReadDTO>(affiliatePartners);
            return new ResponseDto()
            {
                IsSuccess = true,
                Data = affiliatePartnersReadDto,
            };
        }

        public async Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<AffiliatePartnerCreateDTO> request)
        {
            var experts = _mapper.Map<AffiliatePartner>(request);

            var existingAffiliatePartners = await _sender.Send(new GetAffiliatePartnerByIdQuery(Id));
            if (existingAffiliatePartners == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    //   Data = _mapper.Map<ExpertsReadDto>(existingExperts),
                    ErrorMessages = new List<string>() { AppConstants.AffiliatePartner_AffiliatePartnerNotFound }
                };
            }

            var result = await _sender.Send(new PatchAffiliatePartnerCommand(Id, request, existingAffiliatePartners));
            if (result == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = _mapper.Map<AffiliatePartnerReadDTO>(existingAffiliatePartners),
                    ErrorMessages = new List<string>() { AppConstants.AffiliatePartner_FailedToUpdateAffiliatePartner }
                };
            }

            return new ResponseDto()
            {
                Data = _mapper.Map<AffiliatePartnerReadDTO>(result),
                DisplayMessage = AppConstants.AffiliatePartner_AffiliatePartnerUpdated
            };
        }

        public async Task<ResponseDto> Post(AffiliatePartnerCreateDTO request)
        {
            var affiliatePartners = _mapper.Map<AffiliatePartner>(request);

            var existingaffiliatePartners = await _sender.Send(new GetAffiliatePartnerMobileNumberOrEmailQuery(affiliatePartners));
            if (existingaffiliatePartners != null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = _mapper.Map<AffiliatePartnerReadDTO>(existingaffiliatePartners),
                    ErrorMessages = new List<string>() { AppConstants.AffiliatePartner_AffiliatePartnerExistsWithMobileOrEmail }
                };
            }

            var result = await _sender.Send(new CreateAffiliatePartnerCommand(affiliatePartners));
            if (result == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = _mapper.Map<AffiliatePartnerReadDTO>(existingaffiliatePartners),
                    ErrorMessages = new List<string>() { AppConstants.AffiliatePartner_FailedToCreateAffiliatePartner }
                };
            }

            var resultDto = _mapper.Map<AffiliatePartnerReadDTO>(result);
            return new ResponseDto()
            {
                Data = resultDto,
                DisplayMessage = AppConstants.AffiliatePartner_AffiliatePartnerCreated
            };
        }

        public async Task<ResponseDto> Put(Guid id, AffiliatePartnerCreateDTO request)
        {
            var affiliatePartners = _mapper.Map<AffiliatePartner>(request);

            var existingaffiliatePartners = await _sender.Send(new GetAffiliatePartnerByIdQuery(id));
            if (existingaffiliatePartners == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = _mapper.Map<AffiliatePartnerReadDTO>(existingaffiliatePartners),
                    ErrorMessages = new List<string>() { AppConstants.Common_NoRecordFound }
                };
            }
            affiliatePartners.Id = id; // Assigning the provided Id to the experts
            var result = await _sender.Send(new PutAffiliatePartnerCommand(affiliatePartners));
            if (result == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = _mapper.Map<AffiliatePartnerReadDTO>(existingaffiliatePartners),
                    ErrorMessages = new List<string>() { AppConstants.AffiliatePartner_FailedToCreateAffiliatePartner }
                };
            }

            var resultDto = _mapper.Map<AffiliatePartnerReadDTO>(result);
            return new ResponseDto()
            {
                Data = resultDto,
                DisplayMessage = AppConstants.AffiliatePartner_AffiliatePartnerCreated
            };
        }
    }
}
