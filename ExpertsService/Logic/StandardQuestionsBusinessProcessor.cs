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
    public class StandardQuestionsBusinessProcessor : IStandardQuestionsBusinessProcessor
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public StandardQuestionsBusinessProcessor(ISender sender, IMapper mapper)
        {
            this._sender = sender;
            this._mapper = mapper;
        }


        public Task<ResponseDto> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDto> Get(int page = 1, int pageSize = 10)
        {
            var standardQuestionList = await _sender.Send(new GetStandardQuestionsQuery(page, pageSize));
            var standardQuestionsReadDtoList = _mapper.Map<List<StandardQuestionsReadDto>>(standardQuestionList);
            return new ResponseDto()
            {
                IsSuccess = true,
                Data = standardQuestionsReadDtoList,
            };
        }

        public async Task<ResponseDto> Get(Guid id)
        {
            var standardQuestions = await _sender.Send(new GetStandardQuestionByIdQuery(id));
            if (standardQuestions == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = null,
                    ErrorMessages = new List<string>() { AppConstants.Expert_ExpertNotFound }
                };
            }
            var standardQuestionsReadDto = _mapper.Map<StandardQuestionsReadDto>(standardQuestions);
            return new ResponseDto()
            {
                IsSuccess = true,
                Data = standardQuestionsReadDto,
            };
        }

        public async Task<ResponseDto> Get(Guid id, int page = 1, int pageSize = 10)
        {
            var standardQuestionList = await _sender.Send(new GetStandardQuestionsByExpertIdQuery(id, page, pageSize));
            var standardQuestionsReadDtoList = _mapper.Map<List<StandardQuestionsReadDto>>(standardQuestionList);
            return new ResponseDto()
            {
                IsSuccess = true,
                Data = standardQuestionsReadDtoList,
            };
        }

        public async Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<StandardQuestionsCreateDto> request)
        {
            var experts = _mapper.Map<StandardQuestions>(request);

            var existingStandardQuestions = await _sender.Send(new GetStandardQuestionByIdQuery(Id));
            if (existingStandardQuestions == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    //   Data = _mapper.Map<ExpertsReadDto>(existingExperts),
                    ErrorMessages = new List<string>() { AppConstants.Expert_ExpertNotFound }
                };
            }

            var result = await _sender.Send(new PatchStandardQuestionsCommand(Id, request, existingStandardQuestions));
            if (result == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = _mapper.Map<StandardQuestionsReadDto>(existingStandardQuestions),
                    ErrorMessages = new List<string>() { AppConstants.Expert_FailedToUpdateExpert }
                };
            }

            return new ResponseDto()
            {
                Data = _mapper.Map<StandardQuestionsReadDto>(result),
                DisplayMessage = AppConstants.Expert_ExpertUpdated
            };
        }

        public async Task<ResponseDto> Post(StandardQuestionsCreateDto request)
        {
            var standardQuestions = _mapper.Map<StandardQuestions>(request);

            var existingStandardQuestions = await _sender.Send(new GetStandardQuestionByIdQuery(standardQuestions.Id));
            if (existingStandardQuestions != null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    //   Data = _mapper.Map<ExpertsReadDto>(existingExperts),
                    ErrorMessages = new List<string>() { AppConstants.Common_NoRecordFound }
                };
            }

            var result = await _sender.Send(new CreateStandardQuestionsCommand(standardQuestions));
            if (result == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = _mapper.Map<StandardQuestionsReadDto>(existingStandardQuestions),
                    ErrorMessages = new List<string>() { AppConstants.Common_FailedToCreateNewRecord }
                };
            }

            var resultDto = _mapper.Map<StandardQuestionsReadDto>(result);
            return new ResponseDto()
            {
                Data = resultDto,
                DisplayMessage = AppConstants.Common_RecordCreated
            };
        }
    }
}
