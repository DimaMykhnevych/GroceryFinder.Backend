using GroceryFinder.BusinessLayer.Mappers;

namespace GroceryFinder.Web.Installers;

public class MapperInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(MappingProfile));
    }
}

