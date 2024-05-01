using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using ExpertService.Dtos;
using CommonLibrary.CommonDTOs;
using MigrationDB.Models;
using BlogService.Dtos;
using MigrationDB.Model;
using AdvertisingAgencyService.Dtos;

namespace AdvertisingAgencyService.Profiles;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // Source -> Target
        CreateMap<AdvertisingAgency, AdAgencyDetailsReadDto>().ReverseMap();
        CreateMap<AdvertisingAgency, AdAgencyDetailsCreateDto>().ReverseMap();
        CreateMap<AdvertisingAgency, JsonPatchDocument<AdAgencyDetailsCreateDto>>().ReverseMap();
        CreateMap<AdvertisingAgency, ResponseDto>()
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src)); // Map Blog entity to ResponseDto's Data property


    }

}

