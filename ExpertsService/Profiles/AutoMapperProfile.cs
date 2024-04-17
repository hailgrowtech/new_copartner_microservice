using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using ExpertService.Dtos;
using ExpertService.Models;
using CommonLibrary.CommonDTOs;

namespace ExpertService.Profiles;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // Source -> Target
        CreateMap<Experts, ExpertReadDto>().ReverseMap();
        CreateMap<Experts, ExpertsCreateDto>().ReverseMap();
        CreateMap<Experts, JsonPatchDocument<ExpertsCreateDto>>().ReverseMap();
        CreateMap<Experts, ResponseDto>()
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src)); // Map Subscription entity to ResponseDto's Data property

        
    }

}

