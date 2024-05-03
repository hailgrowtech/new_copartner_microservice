using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using CommonLibrary.CommonDTOs;
using MigrationDB.Models;
using AdminDashboardService.Dtos;
using MigrationDB.Model;

namespace AdminDashboardService.Profiles;
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

