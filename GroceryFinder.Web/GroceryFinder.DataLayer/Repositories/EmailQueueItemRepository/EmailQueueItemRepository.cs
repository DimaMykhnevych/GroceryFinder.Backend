using GroceryFinder.DataLayer.DbContext;
using GroceryFinder.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace GroceryFinder.DataLayer.Repositories.EmailQueueItemRepository;

public class EmailQueueItemRepository : Repository<EmailQueueItem>, IEmailQueueItemRepository
{
    public EmailQueueItemRepository(GroceryFinderDbContext context) : base(context)
    {
    }


    public async Task<IEnumerable<EmailQueueItem>> GetNotSentItemsAsync()
    {
        return await context.EmailQueueItems
            .AsNoTracking()
            .Where(i => !i.IsSent)
            .ToListAsync();
    }
}

