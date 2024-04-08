using AutoMapper;
using CommonLibrary;
using CommonLibrary.CommonDTOs;
using CourseService.Commands;
using CourseService.Dtos;
using CourseService.Models;
using CourseService.Queries;
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

        public async Task<ResponseDto> Get()
        {
            var courseList = await _sender.Send(new GetCourseQuery());
            var courseReadDtoList = _mapper.Map<List<CourseReadDto>>(courseList);
            return new ResponseDto()
            {
                IsSuccess = true,
                Data = courseReadDtoList,
            };
        }

        public async Task<ResponseDto> Get(Guid id)
        {
            var courses = await _sender.Send(new GetCourseByIdQuery(id));
            if (courses == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = null,
                    ErrorMessages = new List<string>() { AppConstants.Course_CourseNotFound }
                };
            }
            var expertsReadDto = _mapper.Map<CourseReadDto>(courses);
            return new ResponseDto()
            {
                IsSuccess = true,
                Data = expertsReadDto,
            };
        }

        public async Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<CourseCreateDto> request)
        {
            var courses = _mapper.Map<Course>(request);

            var existingExperts = await _sender.Send(new GetCourseByIdQuery(courses.Id));
            if (existingExperts == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    //   Data = _mapper.Map<UserReadDto>(existingUser),
                    ErrorMessages = new List<string>() { AppConstants.Course_CourseNotFound }
                };
            }

            var result = await _sender.Send(new PatchCourseCommand(Id, request, existingExperts));
            if (result == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = _mapper.Map<CourseReadDto>(existingExperts),
                    ErrorMessages = new List<string>() { AppConstants.User_FailedToUpdateUser }
                };
            }

            return new ResponseDto()
            {
                Data = _mapper.Map<CourseReadDto>(result),
                DisplayMessage = AppConstants.Course_CourseUpdated
            };
        }

        public async Task<ResponseDto> Post(CourseCreateDto request)
        {
            var course = _mapper.Map<Course>(request);

            var existingCourses = await _sender.Send(new GetCourseByIdQuery(course.Id));
            if (existingCourses != null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = _mapper.Map<CourseReadDto>(existingCourses),
                    ErrorMessages = new List<string>() { AppConstants.Course_CourseNotFound }
                };
            }

            var result = await _sender.Send(new CreateCourseCommand(course));
            if (result == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = _mapper.Map<CourseReadDto>(existingCourses),
                    ErrorMessages = new List<string>() { AppConstants.Course_FailedToCreateNewCourse }
                };
            }

            var resultDto = _mapper.Map<CourseReadDto>(result);
            return new ResponseDto()
            {
                Data = resultDto,
                DisplayMessage = AppConstants.Course_CourseCreated
            };
        }
    }
}
