using GroceryFinder.BusinessLayer.Factories;
using GroceryFinder.BusinessLayer.Services.AuthorizationService;
using GroceryFinder.BusinessLayer.Services.EmailService;
using GroceryFinder.BusinessLayer.Services.GroceryStoreService;
using GroceryFinder.BusinessLayer.Services.ProductGroceryStoreService;
using GroceryFinder.BusinessLayer.Services.ProductService;
using GroceryFinder.BusinessLayer.Services.UserAllergyService;
using GroceryFinder.BusinessLayer.Services.UserService;
using GroceryFinder.DataLayer.Builders.GroceryStoreQueryBuilder;
using GroceryFinder.DataLayer.Repositories.GroceryStoreRepository;
using GroceryFinder.DataLayer.Repositories.ProductGroceryStoreRepository;
using GroceryFinder.DataLayer.Repositories.ProductRepository;
using GroceryFinder.DataLayer.Repositories.UserAllergyRepository;
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
        services.AddTransient<IProductService, ProductService>();
        services.AddTransient<IGroceryStoreService, GroceryStoreService>();
        services.AddTransient<IProductGroceryStoreService, ProductGroceryStoreService>();
        services.AddTransient<IUserAllergyService, UserAllergyService>();

        // builders
        services.AddTransient<IGroceryStoreQueryBuilder, GroceryStoreQueryBuilder>();

        // repositories
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IProductRepository, ProductRepository>();
        services.AddTransient<IGroceryStoreRepository, GroceryStoreRepository>();
        services.AddTransient<IProductGroceryStoreRepository, ProductGroceryStoreRepository>();
        services.AddTransient<IUserAllergyRepository, UserAllergyRepository>();
    }
}

