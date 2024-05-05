using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GroceryFinder.DataLayer.Models.Auth;

public class AuthOptions
{
    public const string ISSUER = "Grocery.Finder.API";
    public const string AUDIENCE = "Grocery.Finder.User";
    private const string SECRET_KEY = "4HqoFK424mTaaV3rOWq3uBy0z3JVc8Yh";
    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SECRET_KEY));
    }
}

