namespace GroceryFinder.Web.Options;

public class MySqlConfigOptions
{
    [ConfigurationKeyName("Default")]
    public string DefaultConnectionString { get; set; }
}

