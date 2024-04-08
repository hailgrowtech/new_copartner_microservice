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
    public class CourseBookingBusinessProcessor : ICourseBookingBusinessProcessor
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public CourseBookingBusinessProcessor(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        public async Task<ResponseDto> Get()
        {
            var courseBookingList = await _sender.Send(new GetCourseBookingQuery());
            var courseBookingReadDtoList = _mapper.Map<List<CourseBookingReadDto>>(courseBookingList);
            return new ResponseDto()
            {
                IsSuccess = true,
                Data = courseBookingReadDtoList,
            };
        }

        public async Task<ResponseDto> Get(Guid id)
        {
            var courseBookings = await _sender.Send(new GetCourseBookingByIdQuery(id));
            if (courseBookings == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = null,
                    ErrorMessages = new List<string>() { AppConstants.Course_CourseNotFound }
                };
            }
            var expertsReadDto = _mapper.Map<CourseBookingReadDto>(courseBookings);
            return new ResponseDto()
            {
                IsSuccess = true,
                Data = expertsReadDto,
            };
        }

        public async Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<CourseBookingCreateDto> request)
        {
            var courseBooking = _mapper.Map<CourseBooking>(request);

            var existingCourseBookings = await _sender.Send(new GetCourseBookingByIdQuery(courseBooking.Id));
            if (existingCourseBookings == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    //   Data = _mapper.Map<UserReadDto>(existingUser),
                    ErrorMessages = new List<string>() { AppConstants.Course_CourseNotFound }
                };
            }

            var result = await _sender.Send(new PatchCourseBookingCommand(Id, request, existingCourseBookings));
            if (result == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = _mapper.Map<CourseBookingReadDto>(existingCourseBookings),
                    ErrorMessages = new List<string>() { AppConstants.User_FailedToUpdateUser }
                };
            }

            return new ResponseDto()
            {
                Data = _mapper.Map<CourseBookingReadDto>(result),
                DisplayMessage = AppConstants.Course_CourseUpdated
            };
        }

        public async Task<ResponseDto> Post(CourseBookingCreateDto request)
        {
            var courseBooking = _mapper.Map<CourseBooking>(request);

            var existingCourseBooking = await _sender.Send(new GetCourseByIdQuery(courseBooking.Id));
            if (existingCourseBooking != null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = _mapper.Map<CourseBookingReadDto>(existingCourseBooking),
                    ErrorMessages = new List<string>() { AppConstants.Course_CourseNotFound }
                };
            }

            var result = await _sender.Send(new CreateCourseBookingCommand(courseBooking));
            if (result == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = _mapper.Map<CourseBookingReadDto>(existingCourseBooking),
                    ErrorMessages = new List<string>() { AppConstants.Course_FailedToCreateNewCourse }
                };
            }

            var resultDto = _mapper.Map<CourseBookingReadDto>(result);
            return new ResponseDto()
            {
                Data = resultDto,
                DisplayMessage = AppConstants.Course_CourseCreated
            };
        }
    }
}
