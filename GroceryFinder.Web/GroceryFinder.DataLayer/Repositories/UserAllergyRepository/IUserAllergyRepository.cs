using GroceryFinder.DataLayer.Models;

namespace GroceryFinder.DataLayer.Repositories.UserAllergyRepository;

public interface IUserAllergyRepository : IRepository<UserAllergy>
{
    Task<IEnumerable<UserAllergy>> GetUserAlergiesAsync(Guid userId);
}

