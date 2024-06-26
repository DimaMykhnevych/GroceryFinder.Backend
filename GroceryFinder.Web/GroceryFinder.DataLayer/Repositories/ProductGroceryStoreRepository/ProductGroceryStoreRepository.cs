﻿using GroceryFinder.DataLayer.DbContext;
using GroceryFinder.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace GroceryFinder.DataLayer.Repositories.ProductGroceryStoreRepository;

public class ProductGroceryStoreRepository : Repository<ProductGroceryStore>, IProductGroceryStoreRepository
{
    public ProductGroceryStoreRepository(GroceryFinderDbContext context) : base(context)
    {
    }

    public async Task<ProductGroceryStore> GetProductGroceryStoreAsNoTracking(Guid id)
    {
        return await context.ProductGroceryStores
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
    }
}

