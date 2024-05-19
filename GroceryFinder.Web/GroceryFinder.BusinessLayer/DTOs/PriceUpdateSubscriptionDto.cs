namespace GroceryFinder.BusinessLayer.DTOs;

public class PriceUpdateSubscriptionDto
{
    public Guid Id { get; set; }
    public Guid AppUserId { get; set; }
    public Guid ProductId { get; set; }
    public ProductDto Product { get; set; }
}

