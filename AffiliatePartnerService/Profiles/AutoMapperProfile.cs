﻿using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using ExpertService.Dtos;
using CommonLibrary.CommonDTOs;
using MigrationDB.Models;
using AffiliatePartnerService.Dtos;
using MigrationDB.Model;

namespace AffiliatePartnerService.Profiles;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // Source -> Target
        CreateMap<AffiliatePartner, AffiliatePartnerReadDTO>().ReverseMap();
        CreateMap<AffiliatePartner, AffiliatePartnerCreateDTO>().ReverseMap();
        CreateMap<AffiliatePartner, JsonPatchDocument<AffiliatePartnerCreateDTO>>().ReverseMap();
        CreateMap<AffiliatePartner, ResponseDto>()
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src)); // Map AffiliatePartner entity to ResponseDto's Data property


    }

}

