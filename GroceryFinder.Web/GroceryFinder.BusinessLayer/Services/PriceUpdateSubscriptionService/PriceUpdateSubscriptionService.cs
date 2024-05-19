using AutoMapper;
using GroceryFinder.BusinessLayer.DTOs;
using GroceryFinder.DataLayer.Models;
using GroceryFinder.DataLayer.Repositories.PriceUpdateSubscriptionRepository;

namespace GroceryFinder.BusinessLayer.Services.PriceUpdateSubscriptionService;

public class PriceUpdateSubscriptionService : IPriceUpdateSubscriptionService
{
    private readonly IPriceUpdateSubscriptionRepository _priceUpdateSubscriptionRepository;
    private readonly IMapper _mapper;

    public PriceUpdateSubscriptionService(IPriceUpdateSubscriptionRepository priceUpdateSubscriptionRepository, IMapper mapper)
    {
        _priceUpdateSubscriptionRepository = priceUpdateSubscriptionRepository;
        _mapper = mapper;
    }

    public async Task<PriceUpdateSubscriptionDto> Get(Guid id)
    {
        var subscription = await _priceUpdateSubscriptionRepository.GetSubscriptionWithProductInfo(id);
        return _mapper.Map<PriceUpdateSubscriptionDto>(subscription);
    }

    public async Task<IEnumerable<PriceUpdateSubscriptionDto>> GetAll(Guid userId)
    {
        var subscriptions = await _priceUpdateSubscriptionRepository.GetUserSubscriptionsWithProductInfo(userId);
        return _mapper.Map<IEnumerable<PriceUpdateSubscriptionDto>>(subscriptions);
    }

    public async Task<PriceUpdateSubscriptionDto> Add(Guid userId, AddPriceUpdateSubscriptionDto priceUpdateSubscriptionDto)
    {
        PriceUpdateSubscription subscriptionToAdd = new()
        {
            Id = Guid.NewGuid(),
            ProductId = priceUpdateSubscriptionDto.ProductId,
            AppUserId = userId,
        };

        var addedSubscription = await _priceUpdateSubscriptionRepository.Insert(subscriptionToAdd);
        await _priceUpdateSubscriptionRepository.Save();

        var subscriptionWithProductInfo = await _priceUpdateSubscriptionRepository
            .GetSubscriptionWithProductInfo(addedSubscription.Id);
        return _mapper.Map<PriceUpdateSubscriptionDto>(subscriptionWithProductInfo);
    }

    public async Task Delete(Guid id)
    {
        var subscription = await _priceUpdateSubscriptionRepository.Get(id);
        _priceUpdateSubscriptionRepository.Delete(subscription);
        await _priceUpdateSubscriptionRepository.Save();
    }
}

