using GroceryFinder.DataLayer.DbContext;
using GroceryFinder.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace GroceryFinder.DataLayer.Repositories.PriceUpdateSubscriptionRepository;

public class PriceUpdateSubscriptionRepository : Repository<PriceUpdateSubscription>, IPriceUpdateSubscriptionRepository
{
    public PriceUpdateSubscriptionRepository(GroceryFinderDbContext context) : base(context)
    {
    }

    public async Task<PriceUpdateSubscription> GetSubscriptionWithProductInfo(Guid id)
    {
        return await context.PriceUpdateSubscription
            .Include(p => p.Product)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<PriceUpdateSubscription>> GetSubscriptionsWithUserAndProductInfo()
    {
        return await context.PriceUpdateSubscription
            .AsNoTracking()
            .Include(p => p.AppUser)
            .AsNoTracking()
            .Include(p => p.Product)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<PriceUpdateSubscription>> GetUserSubscriptionsWithProductInfo(Guid userId)
    {
        return await context.PriceUpdateSubscription
            .Include(p => p.Product)
            .Where(p => p.AppUserId == userId)
            .ToListAsync();
    }
}

