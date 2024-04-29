using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using ExpertService.Dtos;
using CommonLibrary.CommonDTOs;
using MigrationDB.Models;
using BlogService.Dtos;
using MigrationDB.Model;

namespace BlogService.Profiles;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // Source -> Target
        CreateMap<Blog, BlogReadDto>().ReverseMap();
        CreateMap<Blog, BLogCreateDto>().ReverseMap();
        CreateMap<Blog, JsonPatchDocument<BLogCreateDto>>().ReverseMap();
        CreateMap<Blog, ResponseDto>()
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src)); // Map Blog entity to ResponseDto's Data property


    }

}

