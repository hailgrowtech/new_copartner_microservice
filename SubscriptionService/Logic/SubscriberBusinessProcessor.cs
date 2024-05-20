using AutoMapper;
using CommonLibrary;
using CommonLibrary.CommonDTOs;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using MigrationDB.Data;
using MigrationDB.Model;
using SubscriptionService.Commands;
using SubscriptionService.Dtos;
using SubscriptionService.Queries;

namespace SubscriptionService.Logic
{
    public class SubscriberBusinessProcessor : ISubscriberBusinessProcessor
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;
        private readonly CoPartnerDbContext _dbContext;

        public SubscriberBusinessProcessor(ISender sender, IMapper mapper, CoPartnerDbContext dbContext)
        {
            _sender = sender;
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<ResponseDto> Get()
        {
            var subscriberList = await _sender.Send(new GetSubscriberQuery());
            var subscriberReadDtoList = _mapper.Map<List<SubscriberReadDto>>(subscriberList);
            return new ResponseDto()
            {
                IsSuccess = true,
                Data = subscriberReadDtoList,
            };
        }

        public async Task<ResponseDto> Get(Guid id)
        {
            var subscribers = await _sender.Send(new GetSubscriberIdQuery(id));
            if (subscribers == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = null,
                    ErrorMessages = new List<string>() { AppConstants.Expert_ExpertNotFound }
                };
            }
            var subscriptionMstsReadDto = _mapper.Map<SubscriberReadDto>(subscribers);
            return new ResponseDto()
            {
                IsSuccess = true,
                Data = subscribers,
            };
        }
        public async Task<ResponseDto> GetByUserId(Guid id)
        {
            var subscribers = await _sender.Send(new GetSubscriberIdQuery(id));
            if (subscribers == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = null,
                    ErrorMessages = new List<string>() { AppConstants.Common_NoRecordFound }
                };
            }
            var subscriptionMstsReadDto = _mapper.Map<SubscriberReadDto>(subscribers);
            return new ResponseDto()
            {
                IsSuccess = true,
                Data = subscribers,
            };
        }

        public async Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<SubscriberCreateDto> request)
        {
            var subscribers = _mapper.Map<Subscriber>(request);

            var existingsubscribers = await _sender.Send(new GetSubscriberIdQuery(Id));
            if (existingsubscribers == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    //   Data = _mapper.Map<ExpertsReadDto>(existingExperts),
                    ErrorMessages = new List<string>() { AppConstants.Expert_ExpertNotFound }
                };
            }

            var result = await _sender.Send(new PatchSubscriberCommand(Id, request, existingsubscribers));
            if (result == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = _mapper.Map<SubscriberReadDto>(existingsubscribers),
                    ErrorMessages = new List<string>() { AppConstants.Expert_FailedToUpdateExpert }
                };
            }

            return new ResponseDto()
            {
                Data = _mapper.Map<SubscriberReadDto>(result),
                DisplayMessage = AppConstants.Expert_ExpertUpdated
            };
        }

        public async Task<ResponseDto> Post(SubscriberCreateDto request)
        {
            var subscribers = _mapper.Map<Subscriber>(request);

            var existingsubscribers = await _sender.Send(new GetSubscriberIdQuery(subscribers.Id));
            if (existingsubscribers != null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = _mapper.Map<SubscriberReadDto>(existingsubscribers),
                    ErrorMessages = new List<string>() { AppConstants.Expert_ExpertExistsWithMobileOrEmail }
                };
            }

            var result = await _sender.Send(new CreateSubscriberCommand(subscribers));
            if (result == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = _mapper.Map<SubscriberReadDto>(existingsubscribers),
                    ErrorMessages = new List<string>() { AppConstants.AffiliatePartner_FailedToCreateAffiliatePartner }
                };
            }

            var resultDto = _mapper.Map<SubscriberReadDto>(result);
            return new ResponseDto()
            {
                Data = resultDto,
                DisplayMessage = AppConstants.Expert_ExpertCreated
            };
        }
        public async Task<ResponseDto> Delete(Guid Id)
        {
            var user = await _sender.Send(new DeleteSubscriptionCommand(Id));
            var userReadDto = _mapper.Map<ResponseDto>(user);
            return userReadDto;
        }

        public async Task<ResponseDto> Put(Guid id, SubscriberCreateDto request)
        {
            var subscriber = _mapper.Map<Subscriber>(request);

            var existingSubscriber = await _sender.Send(new GetSubscriberIdQuery(id));
            if (existingSubscriber == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = _mapper.Map<SubscriberReadDto>(existingSubscriber),
                    ErrorMessages = new List<string>() { AppConstants.Common_NoRecordFound }
                };
            }
            subscriber.Id = id; // Assigning the provided Id to the subscription
            var result = await _sender.Send(new PutSubscriberCommand(subscriber));
            if (result == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Data = _mapper.Map<SubscriberReadDto>(existingSubscriber),
                    ErrorMessages = new List<string>() { AppConstants.AffiliatePartner_FailedToCreateAffiliatePartner }
                };
            }

            var resultDto = _mapper.Map<SubscriberReadDto>(result);
            return new ResponseDto()
            {
                Data = resultDto,
                DisplayMessage = AppConstants.Expert_ExpertCreated
            };
        }

        public void ProcessSubscriberWallet(Guid subscriberId)
        {
            var subscriber = _dbContext.Subscribers.Find(subscriberId);

            if (subscriber == null)
            {
                throw new ArgumentException("Subscriber not found.");
            }

            var user = _dbContext.Users.Find(subscriber.UserId);

            if (user == null)
            {
                throw new ArgumentException("User not found.");
            }

            var subscription = _dbContext.Subscriptions.Find(subscriber.SubscriptionId);

            if (subscription == null)
            {
                throw new ArgumentException("subscription not found.");
            }

            var referralMode = user.ReferralMode;
            Guid? expertsId = subscription.ExpertsId;
            Guid? affiliatePartnerId = user.AffiliatePartnerId;
            decimal amount = subscriber.TotalAmount;
            decimal raAmount = 0, apAmount = 0, cpAmount = 0;

            if (referralMode == "RA")
            {

                if (expertsId != null)
                {
                    raAmount = amount;
                }
                else
                {
                    var expert = _dbContext.Experts.Find(expertsId);
                    raAmount = amount * ((decimal)(expert.FixCommission) / 100);
                }

                 cpAmount = amount - raAmount;

                InsertTransaction(subscriber.Id, affiliatePartnerId, expertsId, raAmount, 0, cpAmount);
            }
            else if (referralMode == "AP")
            {

                if (_dbContext.Subscribers.Count(s => s.UserId == user.Id) < 2)
                {
                    var affiliatePartner = _dbContext.AffiliatePartners.Find(affiliatePartnerId);
                    apAmount = amount * ((decimal)(affiliatePartner.FixCommission1) / 100);

                    var expert = _dbContext.Experts.Find(expertsId);
                    raAmount = amount * ((decimal)(expert.FixCommission) / 100);
                }
                else
                {
                    var affiliatePartner = _dbContext.AffiliatePartners.Find(affiliatePartnerId);
                    apAmount = amount * ((decimal)(affiliatePartner.FixCommission2) / 100);

                    var expert = _dbContext.Experts.Find(expertsId);
                    raAmount = amount * ((decimal)(expert.FixCommission) / 100);
                }

                 cpAmount = amount - (apAmount + raAmount);

                InsertTransaction(subscriber.Id, affiliatePartnerId, expertsId, raAmount, apAmount, cpAmount);
            }
            else
            {
                var expert = _dbContext.Experts.FirstOrDefault(e => e.Id == expertsId);
                if (expert != null)
                {
                    raAmount = amount * ((decimal)(expert.FixCommission) / 100);
                    cpAmount = amount - raAmount;
                    InsertTransaction(subscriber.Id, affiliatePartnerId, expertsId, raAmount, apAmount, cpAmount);
                }
            }
        }

        private void InsertTransaction(Guid subscriberId,Guid? affiliatePartnerId,Guid? expertsId, decimal raAmount, decimal apAmount, decimal cpAmount)
        {
            var transaction = new Wallet
            {
                SubscriberId = subscriberId,
                AffiliatePartnerId = affiliatePartnerId,
                ExpertsId = expertsId,
                RAAmount = raAmount,
                APAmount = apAmount,
                CPAmount = cpAmount,
                TransactionDate = DateTime.Now
            };

            _dbContext.Wallets.Add(transaction);
            _dbContext.SaveChanges();
        }
    }
}
