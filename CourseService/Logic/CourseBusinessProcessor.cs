using AutoMapper;
using CommonLibrary.CommonDTOs;
using CourseService.Dtos;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace CourseService.Logic
{
    public class CourseBusinessProcessor : ICourseBusinessProcessor
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public CourseBusinessProcessor(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        public Task<ResponseDto> Get()
        {
            var expertsList = await _sender.Send(new GetExpertsQuery());
            var expertsReadDtoList = _mapper.Map<List<ExpertReadDto>>(expertsList);
            return new ResponseDto()
            {
                IsSuccess = true,
                Data = expertsReadDtoList,
            };
        }

        public Task<ResponseDto> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<CourseCreateDto> expertsDto)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto> Post(CourseCreateDto experts)
        {
            throw new NotImplementedException();
        }
    }
}
