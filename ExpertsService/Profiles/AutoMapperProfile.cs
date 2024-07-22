using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using ExpertService.Dtos;
using CommonLibrary.CommonDTOs;
using MigrationDB.Models;
using ExpertsService.Dtos;
using MigrationDB.Model;

namespace ExpertService.Profiles;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // Source -> Target
        CreateMap<Experts, ExpertReadDto>().ReverseMap();
        CreateMap<Experts, ExpertsCreateDto>().ReverseMap();
        CreateMap<RAListingDto, RAListingReadDto>().ReverseMap();
        CreateMap<RAListingDataDto, RAListingDataReadDto>().ReverseMap();
        CreateMap<RADetailsDto, RADetailsReadDto>().ReverseMap();
        CreateMap<Experts, JsonPatchDocument<ExpertsCreateDto>>().ReverseMap();
        CreateMap<RAListingDataDto, ResponseDto>()
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src));
        CreateMap<Experts, ResponseDto>()
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src)); // Map Subscription entity to ResponseDto's Data property


        // Source -> Target
        CreateMap<ExpertAvailability, ExpertAvailabilityReadDto>().ReverseMap();
        CreateMap<ExpertAvailability, ExpertAvailabilityCreateDto>().ReverseMap();
        CreateMap<ExpertAvailability, JsonPatchDocument<ExpertAvailabilityCreateDto>>().ReverseMap();
        CreateMap<ExpertAvailability, ResponseDto>()
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src)); // Map Subscription entity to ResponseDto's Data property

        // Source -> Target
        CreateMap<StandardQuestions, StandardQuestionsReadDto>().ReverseMap();
        CreateMap<StandardQuestions, StandardQuestionsCreateDto>().ReverseMap();
        CreateMap<StandardQuestions, JsonPatchDocument<StandardQuestionsCreateDto>>().ReverseMap();
        CreateMap<StandardQuestions, ResponseDto>()
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src)); // Map Subscription entity to ResponseDto's Data property

    }

}

