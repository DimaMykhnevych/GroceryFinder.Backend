using Microsoft.AspNetCore.Identity;
namespace GroceryFinder.DataLayer.Models;

public class AppUser : IdentityUser<Guid>
{
    public string Role { get; set; } = Models.Role.User;
    public DateTime RegistryDate { get; set; }

    public ICollection<UserAllergy> Allergies { get; set; }
}

