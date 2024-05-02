using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using CommonLibrary.CommonDTOs;
using MigrationDB.Models;
using MigrationDB.Model;
using MarketingContentService.Dtos;

namespace MarketingContentService.Profiles;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // Source -> Target
        CreateMap<MarketingContent, MarketingContentReadDto>().ReverseMap();
        CreateMap<MarketingContent, MarketingContentCreateDto>().ReverseMap();
        CreateMap<MarketingContent, JsonPatchDocument<MarketingContentCreateDto>>().ReverseMap();
        CreateMap<MarketingContent, ResponseDto>()
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src)); // Map Subscription entity to ResponseDto's Data property

        
    }

}

