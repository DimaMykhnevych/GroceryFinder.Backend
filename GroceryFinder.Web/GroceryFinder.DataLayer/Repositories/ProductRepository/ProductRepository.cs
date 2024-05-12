using GroceryFinder.DataLayer.DbContext;
using GroceryFinder.DataLayer.Models;

namespace GroceryFinder.DataLayer.Repositories.ProductRepository;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(GroceryFinderDbContext context) : base(context)
    {
    }
}

