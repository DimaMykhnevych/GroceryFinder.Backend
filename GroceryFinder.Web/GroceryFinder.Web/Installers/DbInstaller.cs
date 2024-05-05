using GroceryFinder.DataLayer.DbContext;
using Microsoft.EntityFrameworkCore;

namespace GroceryFinder.Web.Installers;

public class DbInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration["ConnectionStrings:Default"];
        services.AddDbContext<GroceryFinderDbContext>(opt =>
                opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
    }
}
