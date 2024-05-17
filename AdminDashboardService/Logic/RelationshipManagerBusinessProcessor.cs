using AdminDashboardService.Commands;
using AdminDashboardService.Dtos;
using AdminDashboardService.Queries;
using AutoMapper;
using CommonLibrary;
using CommonLibrary.CommonDTOs;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using MigrationDB.Model;

namespace AdminDashboardService.Logic
{
    public class RelationshipManagerBusinessProcessor : IRelationshipManagerBusinessProcessor
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public RelationshipManagerBusinessProcessor(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        public Task<ResponseDto> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDto> Get(int page = 1, int pageSize = 10)
        {
            var relationshipManagerList = await _sender.Send(new GetRelationshipManagerQuery(page, pageSize));
            var relationshipManagerReadDtoList = _mapper.Map<List<RelationshipManagerReadDto>>(relationshipManagerList);
            return new ResponseDto()
            {
                IsSuccess = true,
                Data = relationshipManagerReadDtoList,
            };
        }

        public async Task<ResponseDto> Get(Guid id)
        {
            var relationshipManager = await _sender.Send(new GetRelationshipManagerByIdQuery(id));
            if (relationshipManager == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = null,
                    ErrorMessages = new List<string>() { AppConstants.Expert_ExpertNotFound }
                };
            }
            var relationshipManagerReadDto = _mapper.Map<RelationshipManagerReadDto>(relationshipManager);
            return new ResponseDto()
            {
                IsSuccess = true,
                Data = relationshipManagerReadDto,
            };
        }

        public async Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<RelationshipManagerCreateDto> request)
        {
            var relationshipManager = _mapper.Map<RelationshipManager>(request);

            var existingrelationshipManager = await _sender.Send(new GetRelationshipManagerByIdQuery(Id));
            if (existingrelationshipManager == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    ErrorMessages = new List<string>() { AppConstants.Expert_ExpertNotFound }
                };
            }

            var result = await _sender.Send(new PatchRelationshipManagerCommand(Id, request, existingrelationshipManager));
            if (result == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = _mapper.Map<RelationshipManagerReadDto>(existingrelationshipManager),
                    ErrorMessages = new List<string>() { AppConstants.Expert_FailedToUpdateExpert }
                };
            }

            return new ResponseDto()
            {
                Data = _mapper.Map<RelationshipManagerReadDto>(result),
                DisplayMessage = AppConstants.Expert_ExpertUpdated
            };
        }

        public async Task<ResponseDto> Post(RelationshipManagerCreateDto request)
        {
            var relationshipManager = _mapper.Map<RelationshipManager>(request);

            var existingrelationshipManager = await _sender.Send(new GetRelationshipManagerByIdQuery(relationshipManager.Id));
            if (existingrelationshipManager != null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = _mapper.Map<RelationshipManagerReadDto>(existingrelationshipManager),
                    ErrorMessages = new List<string>() { AppConstants.Expert_ExpertExistsWithMobileOrEmail }
                };
            }

            var result = await _sender.Send(new CreateRelationshipMangerCommand(relationshipManager));
            if (result == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = _mapper.Map<RelationshipManagerReadDto>(existingrelationshipManager),
                    ErrorMessages = new List<string>() { AppConstants.AffiliatePartner_FailedToCreateAffiliatePartner }
                };
            }

            var resultDto = _mapper.Map<RelationshipManagerReadDto>(result);
            return new ResponseDto()
            {
                Data = resultDto,
                DisplayMessage = AppConstants.Expert_ExpertCreated
            };
        }

        public async Task<ResponseDto> Put(Guid id, RelationshipManagerCreateDto request)
        {
            var relationshipManager = _mapper.Map<RelationshipManager>(request);

            var existingrelationshipManager = await _sender.Send(new GetRelationshipManagerByIdQuery(id));
            if (existingrelationshipManager == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = _mapper.Map<RelationshipManagerReadDto>(existingrelationshipManager),
                    ErrorMessages = new List<string>() { AppConstants.Common_NoRecordFound }
                };
            }
            relationshipManager.Id = id; // Assigning the provided Id to the experts
            var result = await _sender.Send(new PutRelationshipManagerCommand(relationshipManager));
            if (result == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = _mapper.Map<RelationshipManagerReadDto>(existingrelationshipManager),
                    ErrorMessages = new List<string>() { AppConstants.AffiliatePartner_FailedToCreateAffiliatePartner }
                };
            }

            var resultDto = _mapper.Map<RelationshipManagerReadDto>(result);
            return new ResponseDto()
            {
                Data = resultDto,
                DisplayMessage = AppConstants.Expert_ExpertCreated
            };
        }
    }
}
