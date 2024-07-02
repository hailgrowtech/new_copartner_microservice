using AutoMapper;
using CommonLibrary;
using CommonLibrary.CommonDTOs;
using ExpertService.Commands;
using ExpertService.Dtos;
using ExpertService.Queries;
using ExpertsService.Commands;
using ExpertsService.Dtos;
using ExpertsService.Queries;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using MigrationDB.Model;

namespace ExpertsService.Logic
{
    public class WebinarMstBusinessProcessor : IWebinarMstBusinessProcessor
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public WebinarMstBusinessProcessor(ISender sender, IMapper mapper)
        {
            this._sender = sender;
            this._mapper = mapper;
        }

        public async Task<ResponseDto> Get(int page = 1, int pageSize = 10)
        {
            var webinarMstList = await _sender.Send(new GetWebinarMstQuery(page, pageSize));
            var webinarMstReadDtoList = _mapper.Map<List<WebinarMstReadDto>>(webinarMstList);
            return new ResponseDto()
            {
                IsSuccess = true,
                Data = webinarMstReadDtoList,
            };
        }

        public async Task<ResponseDto> Get(Guid id)
        {
            var webinar = await _sender.Send(new GetWebinarMstByIdQuery(id));
            if (webinar == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = null,
                    ErrorMessages = new List<string>() { AppConstants.Expert_ExpertNotFound }
                };
            }
            var webinarReadDto = _mapper.Map<WebinarMstReadDto>(webinar);
            return new ResponseDto()
            {
                IsSuccess = true,
                Data = webinarReadDto,
            };
        }

        public async Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<WebinarMstCreateDto> request)
        {
            var webinar = _mapper.Map<WebinarMst>(request);

            var existingwebinars = await _sender.Send(new GetWebinarMstByIdQuery(Id));
            if (existingwebinars == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    //   Data = _mapper.Map<ExpertsReadDto>(existingExperts),
                    ErrorMessages = new List<string>() { AppConstants.Expert_ExpertNotFound }
                };
            }

            var result = await _sender.Send(new PatchWebinarMstCommand(Id, request, existingwebinars));
            if (result == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = _mapper.Map<WebinarMstReadDto>(existingwebinars),
                    ErrorMessages = new List<string>() { AppConstants.Expert_FailedToUpdateExpert }
                };
            }

            return new ResponseDto()
            {
                Data = _mapper.Map<WebinarMstReadDto>(result),
                DisplayMessage = AppConstants.Expert_ExpertUpdated
            };
        }

        public async Task<ResponseDto> Post(WebinarMstCreateDto request)
        {
            var webinar = _mapper.Map<WebinarMst>(request);

            var existingwebinars = await _sender.Send(new GetWebinarMstByIdQuery(webinar.Id));
            if (existingwebinars != null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = _mapper.Map<WebinarMstReadDto>(existingwebinars),
                    ErrorMessages = new List<string>() { AppConstants.Expert_ExpertExistsWithMobileOrEmail }
                };
            }

            var result = await _sender.Send(new CreateWebinarMstCommand(webinar));
            if (result == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = _mapper.Map<WebinarMstReadDto>(existingwebinars),
                    ErrorMessages = new List<string>() { AppConstants.AffiliatePartner_FailedToCreateAffiliatePartner }
                };
            }

            var resultDto = _mapper.Map<WebinarMstReadDto>(result);
            return new ResponseDto()
            {
                Data = resultDto,
                DisplayMessage = AppConstants.Expert_ExpertCreated
            };
        }

        public async Task<ResponseDto> Put(Guid id, WebinarMstCreateDto request)
        {
            var webinar = _mapper.Map<WebinarMst>(request);

            var existingWebinars = await _sender.Send(new GetWebinarMstByIdQuery(id));
            if (existingWebinars == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = _mapper.Map<WebinarMstReadDto>(existingWebinars),
                    ErrorMessages = new List<string>() { AppConstants.Common_NoRecordFound }
                };
            }
            webinar.Id = id; // Assigning the provided Id to the experts
            var result = await _sender.Send(new PutWebinarMstCommand(webinar));
            if (result == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = _mapper.Map<WebinarMstReadDto>(existingWebinars),
                    ErrorMessages = new List<string>() { AppConstants.AffiliatePartner_FailedToCreateAffiliatePartner }
                };
            }

            var resultDto = _mapper.Map<WebinarMstReadDto>(result);
            return new ResponseDto()
            {
                Data = resultDto,
                DisplayMessage = AppConstants.Expert_ExpertCreated
            };
        }
    }
}
