﻿using GroceryFinder.DataLayer.Models;

namespace GroceryFinder.BusinessLayer.DTOs;

public class ProductAddedEmailInfoDto
{
    public AppUser Receiver { get; set; }
    public GroceryStoreDto GroceryStore { get; set; }
    public ProductDto Product { get; set; }
    public double Price { get; set; }
}
