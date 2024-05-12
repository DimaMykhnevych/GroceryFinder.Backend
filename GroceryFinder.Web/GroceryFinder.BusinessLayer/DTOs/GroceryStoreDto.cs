namespace GroceryFinder.BusinessLayer.DTOs;

public class GroceryStoreDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string LogoImageUri { get; set; }
    public string ContactInformation { get; set; }
    public string WorkFrom { get; set; }
    public string WorkTo { get; set; }


    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
}

