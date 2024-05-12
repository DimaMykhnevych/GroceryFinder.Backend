using GroceryFinder.DataLayer.DbContext;
using GroceryFinder.DataLayer.Models;

namespace GroceryFinder.DataLayer.Repositories.GroceryStoreRepository;

public class GroceryStoreRepository : Repository<GroceryStore>, IGroceryStoreRepository
{
    public GroceryStoreRepository(GroceryFinderDbContext context) : base(context)
    {
    }
}

