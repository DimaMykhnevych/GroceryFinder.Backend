using GroceryFinder.DataLayer.Models;

namespace GroceryFinder.DataLayer.Repositories.ProductGroceryStoreRepository;

public interface IProductGroceryStoreRepository : IRepository<ProductGroceryStore>
{
    Task<ProductGroceryStore> GetProductGroceryStoreAsNoTracking(Guid id);
}

