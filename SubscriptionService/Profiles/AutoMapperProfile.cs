using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using MigrationDB.Model;
using SubscriptionService.Dtos;

namespace SubscriptionService.Profiles;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // Source -> Target

        CreateMap<SubscriptionMst, SubscriptionReadDto>().ReverseMap();
        CreateMap<SubscriptionMst, SubscriptionCreateDto>().ReverseMap();
        CreateMap<SubscriptionMst, JsonPatchDocument<SubscriptionCreateDto>>().ReverseMap();
    }

}

