using GroceryFinder.Web.Extensions;
using GroceryFinder.Web.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .InstallServices(builder.Configuration);

var app = builder.Build();

SwaggerOptions swaggerOptions = new();
builder.Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);

app.UseSwagger(option => option.RouteTemplate = swaggerOptions.JsonRoute);
app.UseSwaggerUI(option =>
{
    option.SwaggerEndpoint(swaggerOptions.UiEndpoint, swaggerOptions.Description);
});

//app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("CorsPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
