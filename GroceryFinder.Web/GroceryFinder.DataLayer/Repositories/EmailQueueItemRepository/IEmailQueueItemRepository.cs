using GroceryFinder.DataLayer.Models;

namespace GroceryFinder.DataLayer.Repositories.EmailQueueItemRepository;

public interface IEmailQueueItemRepository : IRepository<EmailQueueItem>
{
    Task<IEnumerable<EmailQueueItem>> GetNotSentItemsAsync();
}