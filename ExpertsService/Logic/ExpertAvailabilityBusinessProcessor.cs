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
    public class ExpertAvailabilityBusinessProcessor : IExpertAvailabilityBusinessProcessor
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public ExpertAvailabilityBusinessProcessor(ISender sender, IMapper mapper)
        {
            this._sender = sender;
            this._mapper = mapper;
        }

        public async Task<ResponseDto> Delete(Guid id)
        {
            var expert = await _sender.Send(new DeleteExpertAvailabilityCommand(id));
            var expertReadDto = _mapper.Map<ResponseDto>(expert);
            return expertReadDto;
        }

        public async Task<ResponseDto> Get(int page = 1, int pageSize = 10)
        {
            var expertAvailabilityList = await _sender.Send(new GetExpertAvailabilityQuery(page, pageSize));
            var expertAvailabilityListReadDtoList = _mapper.Map<List<ExpertAvailabilityReadDto>>(expertAvailabilityList);
            return new ResponseDto()
            {
                IsSuccess = true,
                Data = expertAvailabilityListReadDtoList,
            };
        }

        public async Task<ResponseDto> Get(Guid id)
        {
            var expertAvailability = await _sender.Send(new GetExpertAvailabilityByIdQuery(id));
            if (expertAvailability == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = null,
                    ErrorMessages = new List<string>() { AppConstants.Common_NoRecordFound }
                };
            }
            var expertAvailabilityReadDto = _mapper.Map<ExpertAvailabilityReadDto>(expertAvailability);
            return new ResponseDto()
            {
                IsSuccess = true,
                Data = expertAvailabilityReadDto,
            };
        }

        public async Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<ExpertAvailabilityCreateDto> request)
        {
            var experts = _mapper.Map<ExpertAvailability>(request);

            var existingExperts = await _sender.Send(new GetExpertAvailabilityByIdQuery(Id));
            if (existingExperts == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    //   Data = _mapper.Map<ExpertsReadDto>(existingExperts),
                    ErrorMessages = new List<string>() { AppConstants.Common_NoRecordFound }
                };
            }

            var result = await _sender.Send(new PatchExpertAvailabilityCommand(Id, request, existingExperts));
            if (result == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = _mapper.Map<ExpertAvailabilityReadDto>(existingExperts),
                    ErrorMessages = new List<string>() { AppConstants.Expert_FailedToUpdateExpert }
                };
            }

            return new ResponseDto()
            {
                Data = _mapper.Map<ExpertReadDto>(result),
                DisplayMessage = AppConstants.Expert_ExpertUpdated
            };
        }

        public async Task<ResponseDto> Post(ExpertAvailabilityCreateDto request)
        {
            var experts = _mapper.Map<ExpertAvailability>(request);

            var existingExperts = await _sender.Send(new GetExpertAvailabilityByIdQuery(experts.Id));
            if (existingExperts != null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = _mapper.Map<ExpertAvailabilityReadDto>(existingExperts),
                    ErrorMessages = new List<string>() { AppConstants.Expert_ExpertExistsWithMobileOrEmail }
                };
            }

            var result = await _sender.Send(new CreateExpertAvailabilityCommand(experts));
            if (result == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = _mapper.Map<ExpertAvailabilityReadDto>(existingExperts),
                    ErrorMessages = new List<string>() { AppConstants.AffiliatePartner_FailedToCreateAffiliatePartner }
                };
            }

            var resultDto = _mapper.Map<ExpertAvailabilityReadDto>(result);
            return new ResponseDto()
            {
                Data = resultDto,
                DisplayMessage = AppConstants.Expert_ExpertCreated
            };
        }

        public async Task<ResponseDto> Put(Guid id, ExpertAvailabilityCreateDto request)
        {
            var experts = _mapper.Map<ExpertAvailability>(request);

            var existingExperts = await _sender.Send(new GetExpertAvailabilityByIdQuery(id));
            if (existingExperts == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = _mapper.Map<ExpertAvailabilityReadDto>(existingExperts),
                    ErrorMessages = new List<string>() { AppConstants.Common_NoRecordFound }
                };
            }
            experts.Id = id; // Assigning the provided Id to the experts
            var result = await _sender.Send(new PutExpertAvailabilityCommand(experts));
            if (result == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = _mapper.Map<ExpertAvailabilityReadDto>(existingExperts),
                    ErrorMessages = new List<string>() { AppConstants.AffiliatePartner_FailedToCreateAffiliatePartner }
                };
            }

            var resultDto = _mapper.Map<ExpertAvailabilityReadDto>(result);
            return new ResponseDto()
            {
                Data = resultDto,
                DisplayMessage = AppConstants.Expert_ExpertCreated
            };
        }
    }
}
