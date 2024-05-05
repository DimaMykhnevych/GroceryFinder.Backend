using GroceryFinder.DataLayer.Enums;

namespace GroceryFinder.DataLayer.Models.Auth;

public class JWTTokenStatusResult
{
    public string Token { get; set; }
    public bool IsAuthorized { get; set; }
    public UserAuthInfo UserInfo { get; set; }
    public LoginErrorCode LoginErrorCode { get; set; }
}

