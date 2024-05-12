using GroceryFinder.DataLayer.DbContext;
using GroceryFinder.DataLayer.Models;

namespace GroceryFinder.DataLayer.Repositories.ProductGroceryStoreRepository;

public class ProductGroceryStoreRepository : Repository<ProductGroceryStore>, IProductGroceryStoreRepository
{
    public ProductGroceryStoreRepository(GroceryFinderDbContext context) : base(context)
    {
    }
}

