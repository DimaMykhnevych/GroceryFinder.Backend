﻿using GroceryFinder.BusinessLayer.DTOs;

namespace GroceryFinder.BusinessLayer.Services.ProductService;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllProducts();
    Task<ProductDto> GetProduct(Guid id);
    Task<ProductDto> AddProduct(ProductDto productDto);
}

