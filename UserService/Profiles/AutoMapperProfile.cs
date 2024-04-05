using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using UserService.Dtos;
using UserService.Models;

namespace UserService.Profiles;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // Source -> Target
        CreateMap<User, UserReadDto>().ReverseMap();
        CreateMap<User, UserCreateDto>().ReverseMap();
        CreateMap<User, JsonPatchDocument<UserCreateDto>>().ReverseMap();
    }

}

