using GroceryFinder.DataLayer.DbContext;
using GroceryFinder.DataLayer.Models;

namespace GroceryFinder.DataLayer.Repositories.UserAllergyRepository;

public class UserAllergyRepository : Repository<UserAllergy>, IUserAllergyRepository
{
    public UserAllergyRepository(GroceryFinderDbContext context) : base(context)
    {
    }
}

