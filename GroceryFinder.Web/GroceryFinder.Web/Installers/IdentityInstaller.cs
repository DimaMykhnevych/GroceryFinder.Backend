using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using GroceryFinder.DataLayer.DbContext;
using GroceryFinder.DataLayer.Models;
using GroceryFinder.DataLayer.Models.Auth;

namespace GroceryFinder.Web.Installers;

public class IdentityInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentity<AppUser, UserRole>()
            .AddEntityFrameworkStores<GroceryFinderDbContext>()
            .AddDefaultTokenProviders();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            o.IncludeErrorDetails = true;

            o.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateActor = false,
                ValidIssuer = AuthOptions.ISSUER,
                ValidAudience = AuthOptions.AUDIENCE,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
            };
        });

        services.AddAuthorization();
    }
}

