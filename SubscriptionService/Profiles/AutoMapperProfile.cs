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

        CreateMap<SubscriptionMst, SubscriptionMstReadDto>().ReverseMap();
        CreateMap<SubscriptionMst, SubscriptionMstCreateDto>().ReverseMap();
        CreateMap<SubscriptionMst, JsonPatchDocument<SubscriptionMstCreateDto>>().ReverseMap();
    }

}

