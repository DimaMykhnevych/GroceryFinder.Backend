using GroceryFinder.BusinessLayer.DTOs;
using GroceryFinder.DataLayer.Models;

namespace GroceryFinder.BusinessLayer.Services.UserService;

public interface IUserService
{
    Task<AppUser> GetUserByUsername(string username);
    Task<AppUser> CreateUserAsync(CreateUserDto userModel);
    Task DeleteUser(Guid userId);
    Task<ConfirmEmailDto> ConfirmEmail(ConfirmEmailDto confirmEmailDto);
}
