namespace GroceryFinder.DataLayer.Models;

public class UserAllergy
{
    public Guid Id { get; set; }
    public AllergenType AllergenType { get; set; }
    public string Notes { get; set; }

    public Guid AppUserId { get; set; }
    public AppUser AppUser { get; set; }
}