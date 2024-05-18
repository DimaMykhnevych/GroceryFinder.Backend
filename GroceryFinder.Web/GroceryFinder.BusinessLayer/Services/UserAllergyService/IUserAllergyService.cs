using GroceryFinder.BusinessLayer.DTOs;

namespace GroceryFinder.BusinessLayer.Services.UserAllergyService;

public interface IUserAllergyService
{
    Task<UserAllergyDto> Get(Guid id);
    Task<IEnumerable<UserAllergyDto>> GetAll(Guid userId);
    Task<UserAllergyDto> Add(Guid userId, UserAllergyDto userAllergyDto);
    Task<UserAllergyDto> Update(Guid userId, UserAllergyDto userAllergyDto);
    Task Delete(Guid id);
}

