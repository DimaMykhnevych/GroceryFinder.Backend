using GroceryFinder.BusinessLayer.Constants;
using GroceryFinder.BusinessLayer.Options;
using GroceryFinder.Web.Options;

namespace GroceryFinder.Web.Installers;

public class OptionsInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MySqlConfigOptions>(configuration.GetSection(ConfigurationKeys.ConnectionStringsSection));
        services.Configure<EmailServiceOptions>(configuration.GetSection(ConfigurationKeys.EmailServiceOptions));
    }
}
