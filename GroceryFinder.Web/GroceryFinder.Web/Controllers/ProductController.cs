﻿using GroceryFinder.BusinessLayer.DTOs;
using GroceryFinder.BusinessLayer.Services.ProductService;
using GroceryFinder.DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GroceryFinder.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("get-all")]
    [SwaggerOperation(Summary = "Gets all available products")]
    public async Task<IActionResult> GetProducts()
    {
        IEnumerable<ProductDto> products = await _productService.GetAllProducts();
        return Ok(products);
    }

    [HttpPost]
    [Authorize(Roles = Role.Admin)]
    [SwaggerOperation(Summary = "Adds new product",
        Description = "Available only for administrators")]
    public async Task<IActionResult> AddProduct([FromBody] ProductDto productDto)
    {
        ProductDto addedProduct = await _productService.AddProduct(productDto);
        return Ok(addedProduct);
    }
}
