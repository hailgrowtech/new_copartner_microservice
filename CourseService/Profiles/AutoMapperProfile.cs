using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using ExpertService.Dtos;
using ExpertService.Models;

namespace ExpertService.Profiles;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // Source -> Target
        CreateMap<Experts, ExpertReadDto>().ReverseMap();
        CreateMap<Experts, ExpertsCreateDto>().ReverseMap();
        CreateMap<Experts, JsonPatchDocument<ExpertsCreateDto>>().ReverseMap();
    }

}

