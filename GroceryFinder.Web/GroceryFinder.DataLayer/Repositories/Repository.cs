using GroceryFinder.DataLayer.DbContext;
using Microsoft.EntityFrameworkCore;

namespace GroceryFinder.DataLayer.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly GroceryFinderDbContext context;

    public Repository(GroceryFinderDbContext context)
    {
        this.context = context;
    }
    public async Task<TEntity> Get(Guid id)
    {
        return await context.Set<TEntity>().FindAsync(id);
    }

    public async Task<IEnumerable<TEntity>> GetAll()
    {
        return await context.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity> Insert(TEntity entity)
    {
        await context.Set<TEntity>().AddAsync(entity);
        return entity;
    }

    public void Delete(TEntity entity)
    {
        context.Set<TEntity>().Remove(entity);
    }

    public async Task Update(TEntity entity)
    {
        context.Set<TEntity>().Update(entity);
        await Save();
    }

    public async Task Save()
    {
        await context.SaveChangesAsync();
    }
}

