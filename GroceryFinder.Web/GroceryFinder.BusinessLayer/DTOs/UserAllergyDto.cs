namespace GroceryFinder.BusinessLayer.DTOs;

public class UserAllergyDto
{
    public Guid Id { get; set; }
    public AllergenTypeDto AllergenType { get; set; }
    public string Notes { get; set; }
}

