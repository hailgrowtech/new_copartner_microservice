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
    public class JoinBusinessProcessor : IJoinBusinessProcessor
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public JoinBusinessProcessor(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        public async Task<ResponseDto> Delete(Guid id)
        {
            var join = await _sender.Send(new DeleteBlogCommand(id));
            var joinReadDto = _mapper.Map<ResponseDto>(join);
            return joinReadDto;
        }

        public async Task<ResponseDto> Get()
        {
            var joinsList = await _sender.Send(new GetJoinQuery());
            var joinsListReadDtoList = _mapper.Map<List<JoinReadDto>>(joinsList);
            return new ResponseDto()
            {
                IsSuccess = true,
                Data = joinsListReadDtoList,
            };
        }

        public async Task<ResponseDto> Get(Guid id)
        {
            var joins = await _sender.Send(new GetJoinByIdQuery(id));
            if (joins == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = null,
                    ErrorMessages = new List<string>() { AppConstants.Expert_ExpertNotFound }
                };
            }
            var joinsReadDto = _mapper.Map<JoinReadDto>(joins);
            return new ResponseDto()
            {
                IsSuccess = true,
                Data = joinsReadDto,
            };
        }

        public async Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<JoinCreateDto> request)
        {
            var joins = _mapper.Map<Join>(request);

            var existingjoins = await _sender.Send(new GetJoinByIdQuery(Id));
            if (existingjoins == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = null,
                    ErrorMessages = new List<string>() { AppConstants.Expert_ExpertNotFound }
                };
            }

            var result = await _sender.Send(new PatchJoinCommand(Id, request, existingjoins));
            if (result == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = _mapper.Map<JoinReadDto>(existingjoins),
                    ErrorMessages = new List<string>() { AppConstants.Expert_FailedToUpdateExpert }
                };
            }

            return new ResponseDto()
            {
                Data = _mapper.Map<JoinReadDto>(result),
                DisplayMessage = AppConstants.Expert_ExpertUpdated
            };
        }

        public async Task<ResponseDto> Post(JoinCreateDto request)
        {
            var joins = _mapper.Map<Join>(request);

            var existingJoins = await _sender.Send(new GetBlogByIdQuery(joins.Id));
            if (existingJoins != null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = _mapper.Map<JoinReadDto>(existingJoins),
                    ErrorMessages = new List<string>() { AppConstants.Expert_ExpertExistsWithMobileOrEmail }
                };
            }

            var result = await _sender.Send(new CreateJoinCommand(joins));
            if (result == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = _mapper.Map<JoinReadDto>(existingJoins),
                    ErrorMessages = new List<string>() { AppConstants.Expert_FailedToCreateNewExpert }
                };
            }

            var resultDto = _mapper.Map<JoinReadDto>(result);
            return new ResponseDto()
            {
                Data = resultDto,
                DisplayMessage = AppConstants.Expert_ExpertCreated
            };
        }

        public async Task<ResponseDto> Put(Guid id, JoinCreateDto request)
        {
            var joins = _mapper.Map<Join>(request);

            var existingjoins = await _sender.Send(new GetJoinByIdQuery(id));
            if (existingjoins == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = _mapper.Map<JoinReadDto>(existingjoins),
                    ErrorMessages = new List<string>() { AppConstants.Common_NoRecordFound }
                };
            }
            joins.Id = id; // Assigning the provided Id to the experts
            var result = await _sender.Send(new PutJoinCommand(joins));
            if (result == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = _mapper.Map<JoinReadDto>(existingjoins),
                    ErrorMessages = new List<string>() { AppConstants.Expert_FailedToCreateNewExpert }
                };
            }

            var resultDto = _mapper.Map<JoinReadDto>(result);
            return new ResponseDto()
            {
                Data = resultDto,
                DisplayMessage = AppConstants.Expert_ExpertCreated
            };
        }
    }
}
