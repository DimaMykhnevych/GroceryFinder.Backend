using GroceryFinder.DataLayer.DbContext;
using GroceryFinder.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace GroceryFinder.DataLayer.Repositories.UserAllergyRepository;

public class UserAllergyRepository : Repository<UserAllergy>, IUserAllergyRepository
{
    public UserAllergyRepository(GroceryFinderDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<UserAllergy>> GetUserAlergiesAsync(Guid userId)
    {
        return await context.UserAllergy
            .Where(a => a.AppUserId == userId)
            .ToListAsync();
    }
}

