using GroceryFinder.Web.Options;

namespace GroceryFinder.Web.Installers;

public class OptionsInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MySqlConfigOptions>(configuration.GetSection("ConnectionStrings"));
    }
}
