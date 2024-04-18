using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using MigrationDB.Models;
using UserService.Dtos;

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

