﻿using GroceryFinder.BusinessLayer.Constants;
using GroceryFinder.BusinessLayer.Extensions;
using GroceryFinder.BusinessLayer.Factories;
using GroceryFinder.DataLayer.Enums;
using GroceryFinder.DataLayer.Models;
using GroceryFinder.DataLayer.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace GroceryFinder.BusinessLayer.Services.AuthorizationService;

public class AppUserAuthorizationService : BaseAuthorizationService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IConfiguration _configuration;

    public AppUserAuthorizationService(
        IAuthTokenFactory tokenFactory,
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        IConfiguration configuration)
        : base(tokenFactory)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    public override async Task<IEnumerable<Claim>> GetUserClaimsAsync(AuthSignInModel model)
    {
        AppUser user = await _userManager.FindByNameAsync(model.UserName);

        if (user == null)
        {
            return new List<Claim> { };
        }

        return new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.UserName.ToString()),
            new Claim(AuthorizationConstants.ID, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role)
        };
    }

    public async override Task<LoginErrorCode> VerifyUserAsync(AuthSignInModel model)
    {
        AppUser user = await _userManager.FindByNameAsync(model.UserName);
        if (user == null)
        {
            return LoginErrorCode.InvalidUsernameOrPassword;
        }

        if (_configuration.EmailConfirmationEnabled() && !await _userManager.IsEmailConfirmedAsync(user))
        {
            return LoginErrorCode.EmailConfirmationRequired;
        }

        SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

        return result.Succeeded ? LoginErrorCode.None : LoginErrorCode.InvalidUsernameOrPassword;
    }

    public async override Task<UserAuthInfo> GetUserInfoAsync(string userName)
    {
        if (userName == null) return null;
        AppUser user = await _userManager.FindByNameAsync(userName);

        UserAuthInfo info = new ()
        {
            Role = user.Role,
            UserId = user.Id,
            UserName = user.UserName,
            RegistryDate = user.RegistryDate,
            Email = user.Email
        };

        return info;
    }
}