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

        // Source -> Target
        CreateMap<MarketingContent, MarketingContentReadDto>().ReverseMap();
        CreateMap<MarketingContent, MarketingContentCreateDto>().ReverseMap();
        CreateMap<MarketingContent, JsonPatchDocument<MarketingContentCreateDto>>().ReverseMap();
        CreateMap<MarketingContent, ResponseDto>()
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src)); // Map Subscription entity to ResponseDto's Data property

        // Source -> Target
        CreateMap<AdvertisingAgency, AdAgencyDetailsReadDto>().ReverseMap();
        CreateMap<AdvertisingAgency, AdAgencyDetailsCreateDto>().ReverseMap();
        CreateMap<AdvertisingAgency, JsonPatchDocument<AdAgencyDetailsCreateDto>>().ReverseMap();
        CreateMap<AdvertisingAgency, ResponseDto>()
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src)); // Map Blog entity to ResponseDto's Data property
                                                                           // Source -> Target
        CreateMap<ExpertsAdvertisingAgency, ExpertsAdAgencyReadDto>().ReverseMap();
        CreateMap<ExpertsAdvertisingAgency, ExpertsAdAgencyCreateDto>().ReverseMap();
        CreateMap<ExpertsAdvertisingAgency, JsonPatchDocument<ExpertsAdAgencyCreateDto>>().ReverseMap();
        CreateMap<ExpertsAdvertisingAgency, ResponseDto>()
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src)); // Map Blog entity to ResponseDto's Data property

        // Source -> Target
        CreateMap<RelationshipManager, RelationshipManagerReadDto>().ReverseMap();
        CreateMap<RelationshipManager, RelationshipManagerCreateDto>().ReverseMap();
        CreateMap<RelationshipManager, JsonPatchDocument<RelationshipManagerCreateDto>>().ReverseMap();
        CreateMap<RelationshipManager, ResponseDto>()
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src)); // Map Blog entity to ResponseDto's Data property

    }

}

