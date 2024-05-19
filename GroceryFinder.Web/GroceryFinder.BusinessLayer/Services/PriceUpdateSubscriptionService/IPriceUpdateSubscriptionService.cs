using GroceryFinder.BusinessLayer.DTOs;

namespace GroceryFinder.BusinessLayer.Services.PriceUpdateSubscriptionService;

public interface IPriceUpdateSubscriptionService
{
    Task<PriceUpdateSubscriptionDto> Get(Guid id);
    Task<IEnumerable<PriceUpdateSubscriptionDto>> GetAll(Guid userId);
    Task<PriceUpdateSubscriptionDto> Add(Guid userId, AddPriceUpdateSubscriptionDto priceUpdateSubscriptionDto);
    Task Delete(Guid id);
}

