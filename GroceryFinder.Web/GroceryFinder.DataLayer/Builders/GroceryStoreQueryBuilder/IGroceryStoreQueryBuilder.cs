using GroceryFinder.DataLayer.Enums;
using GroceryFinder.DataLayer.Models;

namespace GroceryFinder.DataLayer.Builders.GroceryStoreQueryBuilder;

public interface IGroceryStoreQueryBuilder : IQueryBuilder<GroceryStore>
{
    IGroceryStoreQueryBuilder SetBaseGroceryStoreInfo();
    IGroceryStoreQueryBuilder SetProductToSearch(Guid? productId);
}

