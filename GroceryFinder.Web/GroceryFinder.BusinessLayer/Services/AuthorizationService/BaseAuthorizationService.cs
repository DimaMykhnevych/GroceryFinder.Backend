using GroceryFinder.BusinessLayer.Factories;
using GroceryFinder.DataLayer.Enums;
using GroceryFinder.DataLayer.Models.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GroceryFinder.BusinessLayer.Services.AuthorizationService;

public abstract class BaseAuthorizationService
{
    private readonly IAuthTokenFactory _tokenFactory;
    public BaseAuthorizationService(IAuthTokenFactory tokenFactory)
    {
        _tokenFactory = tokenFactory;
    }
    public async Task<JWTTokenStatusResult> GenerateTokenAsync(AuthSignInModel model)
    {
        LoginErrorCode status = await VerifyUserAsync(model);
        if (status != LoginErrorCode.None)
        {
            return new JWTTokenStatusResult()
            {
                Token = null,
                IsAuthorized = false,
                LoginErrorCode = status
            };
        }

        IEnumerable<Claim> claims = await GetUserClaimsAsync(model);
        JwtSecurityToken token = _tokenFactory.CreateToken(model.UserName.ToString(), claims);
        UserAuthInfo info = await GetUserInfoAsync(model.UserName);

        return new JWTTokenStatusResult()
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            IsAuthorized = true,
            UserInfo = info,
            LoginErrorCode = LoginErrorCode.None
        };
    }

    public abstract Task<IEnumerable<Claim>> GetUserClaimsAsync(AuthSignInModel model);
    public abstract Task<UserAuthInfo> GetUserInfoAsync(string userName);
    public abstract Task<LoginErrorCode> VerifyUserAsync(AuthSignInModel model);
}
