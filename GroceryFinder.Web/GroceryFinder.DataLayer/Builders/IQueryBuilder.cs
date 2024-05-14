namespace GroceryFinder.DataLayer.Builders;

public interface IQueryBuilder<TEntity>
{
    IQueryable<TEntity> Build();
}

