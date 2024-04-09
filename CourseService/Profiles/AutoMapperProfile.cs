using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using CourseService.Models;
using CourseService.Dtos;

namespace ExpertService.Profiles;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // Source -> Target

        CreateMap<Course, CourseReadDto>().ReverseMap();
        CreateMap<Course, CourseCreateDto>().ReverseMap();
        CreateMap<Course, JsonPatchDocument<CourseCreateDto>>().ReverseMap();
    }

}

