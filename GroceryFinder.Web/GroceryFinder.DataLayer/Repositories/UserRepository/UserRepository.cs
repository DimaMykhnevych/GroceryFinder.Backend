using GroceryFinder.DataLayer.DbContext;
using GroceryFinder.DataLayer.Models;

namespace GroceryFinder.DataLayer.Repositories.UserRepository;

public class UserRepository : Repository<AppUser>, IUserRepository
{
    public UserRepository(GroceryFinderDbContext context) : base(context)
    {
    }
}

