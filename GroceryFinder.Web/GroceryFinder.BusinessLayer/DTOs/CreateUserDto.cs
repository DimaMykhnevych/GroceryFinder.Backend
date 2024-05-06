using System.ComponentModel.DataAnnotations;

namespace GroceryFinder.BusinessLayer.DTOs;

public class CreateUserDto
{
    public string Role { get; set; }
    [Required]
    public string UserName { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }
    public string ClientURIForEmailConfirmation { get; set; }
}

