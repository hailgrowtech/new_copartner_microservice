using AffiliatePartnerService.Commands;
using AffiliatePartnerService.Dtos;
using AffiliatePartnerService.Queries;
using AutoMapper;
using CommonLibrary;
using CommonLibrary.CommonDTOs;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using MigrationDB.Model;

namespace AffiliatePartnerService.Logic
{
    public class APListingDetailsBusinessProcessor : IAPDetailsBusinessProcessor
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public APListingDetailsBusinessProcessor(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        public async Task<ResponseDto> GetGeneratedAPLinkById(Guid id)
        {
            var aPGeneratedLinks = await _sender.Send(new GetAPGeneratedLinkListByIdQuery(id));
            if (aPGeneratedLinks == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = null,
                    ErrorMessages = new List<string>() { AppConstants.AffiliatePartner_AffiliatePartnerNotFound }
                };
            }
            //var apGeneratedLinkReadDTOs = _mapper.Map<List<APGeneratedLinkReadDTO>>(aPGeneratedLinks);
            return new ResponseDto()
            {
                IsSuccess = true,
                Data = aPGeneratedLinks,
            };
        }


        public async Task<ResponseDto> GenerateAPLink(APGenerateLinkRequestDto requestDto)
        {

            // Retrieve the AffiliatePartner object using the ID
            var affiliatePartner = await _sender.Send(new GetAffiliatePartnerByIdQuery(requestDto.AffiliatePartnerId));
            if (affiliatePartner == null)
            {
                return new ResponseDto
                {
                    IsSuccess = false,
                    ErrorMessages = new List<string> { AppConstants.AffiliatePartner_AffiliatePartnerNotFound }
                };
            }

            // Proceed with generating the links
            var result = await _sender.Send(new CreateAPGeneratedLinkCommand(requestDto.AffiliatePartnerId, requestDto.Num, requestDto.APReferralLink));
            if (result == null)
            {
                return new ResponseDto
                {
                    IsSuccess = false,
                    ErrorMessages = new List<string> { AppConstants.AffiliatePartner_FailedToCreateAffiliatePartner }
                };
            }

            return new ResponseDto
            {
                IsSuccess = true,
                Data = result,
                DisplayMessage = AppConstants.AffiliatePartner_AffiliatePartnerCreated
            };

        }


        public async Task<ResponseDto> PatchAPGeneratedLink(Guid Id, JsonPatchDocument<APGeneratedLinkCreateDTO> request)
        {
            var apgeneratedlinks = _mapper.Map<APGeneratedLinks>(request);

            var existingAPGeneratedLink = await _sender.Send(new GetAPGeneratedLinkByIdQuery(Id));
            if (existingAPGeneratedLink == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    //   Data = _mapper.Map<ExpertsReadDto>(existingExperts),
                    ErrorMessages = new List<string>() { AppConstants.Expert_ExpertNotFound }
                };
            }

            var result = await _sender.Send(new PatchAPGeneratedLinkCommand(Id, request, existingAPGeneratedLink));
            if (result == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = _mapper.Map<APGeneratedLinkReadDTO>(existingAPGeneratedLink),
                    ErrorMessages = new List<string>() { AppConstants.Expert_FailedToUpdateExpert }
                };
            }

            return new ResponseDto()
            {
                Data = _mapper.Map<APGeneratedLinkReadDTO>(result),
                DisplayMessage = AppConstants.Expert_ExpertUpdated
            };
        }


        public async Task<ResponseDto> Get(int page = 1, int pageSize = 10)
        {
            var apListingDetailsList = await _sender.Send(new GetAPDetailsQuery());
            var apListingDetialsReadDtoList = _mapper.Map<List<APDetailReadDto>>(apListingDetailsList);
            return new ResponseDto()
            {
                IsSuccess = true,
                Data = apListingDetialsReadDtoList,
            };
        }

   
    }
}
