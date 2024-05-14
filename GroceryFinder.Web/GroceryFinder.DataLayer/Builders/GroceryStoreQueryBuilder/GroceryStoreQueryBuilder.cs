using GeoCoordinatePortable;
using GroceryFinder.DataLayer.DbContext;
using GroceryFinder.DataLayer.Enums;
using GroceryFinder.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace GroceryFinder.DataLayer.Builders.GroceryStoreQueryBuilder;

public class GroceryStoreQueryBuilder : IGroceryStoreQueryBuilder
{
    private readonly GroceryFinderDbContext _dbContext;
    private IQueryable<GroceryStore> _query;

    public GroceryStoreQueryBuilder(GroceryFinderDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<GroceryStore> Build()
    {
        IQueryable<GroceryStore> result = _query;
        _query = null;
        return result;
    }

    public IGroceryStoreQueryBuilder SetBaseGroceryStoreInfo()
    {
        _query = _dbContext.GroceryStores
            .Include(s => s.ProductGroceryStores);

        return this;
    }

    public IGroceryStoreQueryBuilder SetProductToSearch(Guid? productId)
    {
        if (productId != null)
        {
            _query = _query.Where(s => s.ProductGroceryStores.Any(gs => gs.ProductId == productId.Value));
        }

        return this;
    }
}

