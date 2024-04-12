using AutoMapper;
using CommonLibrary.CommonDTOs;
using Microsoft.AspNetCore.JsonPatch;
using MigrationDB.Model;
using SubscriptionService.Dtos;

namespace SubscriptionService.Profiles;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // Source -> Target

        CreateMap<Subscription, SubscriptionReadDto>().ReverseMap();
        CreateMap<Subscription, SubscriptionCreateDto>().ReverseMap();
        CreateMap<Subscription, JsonPatchDocument<SubscriptionCreateDto>>().ReverseMap();
        CreateMap<Subscription, ResponseDto>()
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src)); // Map Subscription entity to ResponseDto's Data property
    }

}

