using GroceryFinder.DataLayer.Models;

namespace GroceryFinder.DataLayer.Repositories.PriceUpdateSubscriptionRepository;

public interface IPriceUpdateSubscriptionRepository : IRepository<PriceUpdateSubscription>
{
    Task<PriceUpdateSubscription> GetSubscriptionWithProductInfo(Guid id);
    Task<IEnumerable<PriceUpdateSubscription>> GetUserSubscriptionsWithProductInfo(Guid userId);
    Task<IEnumerable<PriceUpdateSubscription>> GetSubscriptionsWithUserAndProductInfo();
}

