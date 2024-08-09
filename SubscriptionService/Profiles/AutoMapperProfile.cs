﻿using AutoMapper;
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
        // CreateMap<Subscription, SubscriptionReadDto>().ReverseMap();
        CreateMap<Subscription, SubscriptionReadDto>()
            .ForMember(dest => dest.DiscountedAmount, opt => opt.MapFrom(src => CalculateDiscountedAmount(src)));
        CreateMap<Subscription, SubscriptionCreateDto>().ReverseMap();
        CreateMap<Subscription, JsonPatchDocument<SubscriptionCreateDto>>().ReverseMap();
        CreateMap<Subscription, ResponseDto>()
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src)); // Map Subscription entity to ResponseDto's Data property

        // Source -> Target
        CreateMap<Subscriber, SubscriberReadDto>().ReverseMap();
        CreateMap<TempSubscriber, SubscriberReadDto>().ReverseMap();
        CreateMap<Subscriber, SubscriberCreateDto>().ReverseMap();
        CreateMap<TempSubscriber, SubscriberCreateDto>().ReverseMap();
        CreateMap<Subscriber, JsonPatchDocument<SubscriberCreateDto>>().ReverseMap();
        CreateMap<Subscriber, ResponseDto>()
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src)); // Map Subscription entity to ResponseDto's Data property

        // Source -> Target
        CreateMap<PaymentResponse, PaymentResponseReadDto>().ReverseMap();
        CreateMap<PaymentResponse, PaymentResponseCreateDto>().ReverseMap();
        CreateMap<PaymentResponse, JsonPatchDocument<PaymentResponseCreateDto>>().ReverseMap();
        CreateMap<PaymentResponse, ResponseDto>()
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src)); // Map Subscription entity to ResponseDto's Data property

        // Source -> Target
        CreateMap<MinisubscriptionLink, MiniSubscriptionReadDto>().ReverseMap();
        CreateMap<MinisubscriptionLink, MiniSubscriptionCreateDto>().ReverseMap();
        CreateMap<MinisubscriptionLink, JsonPatchDocument<MiniSubscriptionCreateDto>>().ReverseMap();
        CreateMap<MinisubscriptionLink, ResponseDto>()
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src)); // Map Subscription entity to ResponseDto's Data property
    }
    private decimal? CalculateDiscountedAmount(Subscription subscription)
    {
        if (subscription.Amount.HasValue &&
            subscription.DiscountPercentage.HasValue &&
            subscription.DiscountValidFrom.HasValue &&
            subscription.DiscountValidTo.HasValue)
        {
            var currentDate = DateTime.UtcNow;
            if (currentDate >= subscription.DiscountValidFrom.Value && currentDate <= subscription.DiscountValidTo.Value)
            {
                var discount = subscription.Amount.Value * subscription.DiscountPercentage.Value / 100;
                return subscription.Amount.Value - discount;
            }
        }
        return subscription.Amount;
    }

}

