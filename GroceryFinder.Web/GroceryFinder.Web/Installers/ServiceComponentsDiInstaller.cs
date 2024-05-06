using GroceryFinder.BusinessLayer.Factories;
using GroceryFinder.BusinessLayer.Services.AuthorizationService;
using GroceryFinder.BusinessLayer.Services.EmailService;
using GroceryFinder.BusinessLayer.Services.UserService;
using GroceryFinder.DataLayer.Repositories.UserRepository;

namespace GroceryFinder.Web.Installers;

public class ServiceComponentsDiInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        // factories
        services.AddTransient<IAuthTokenFactory, AuthTokenFactory>();

        // services
        services.AddTransient<BaseAuthorizationService, AppUserAuthorizationService>();
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IEmailService, EmailService>();

        // builders

        // repositories
        services.AddTransient<IUserRepository, UserRepository>();
    }
}

