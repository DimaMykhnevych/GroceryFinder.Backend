namespace GroceryFinder.DataLayer.Models;

public class PriceUpdateSubscription
{
    public Guid Id { get; set; }

    public Guid AppUserId { get; set; }
    public Guid ProductId { get; set; }
    public AppUser AppUser { get; set; }
    public Product Product { get; set; }
}