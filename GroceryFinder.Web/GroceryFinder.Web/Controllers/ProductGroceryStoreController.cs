using GroceryFinder.BusinessLayer.DTOs;
using GroceryFinder.BusinessLayer.Services.ProductGroceryStoreService;
using GroceryFinder.DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GroceryFinder.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductGroceryStoreController : ControllerBase
{
    private readonly IProductGroceryStoreService _productGroceryStoreService;
    public ProductGroceryStoreController(IProductGroceryStoreService productGroceryStoreService)
    {
        _productGroceryStoreService = productGroceryStoreService;
    }

    [HttpGet("get-all")]
    [Authorize(Roles = Role.Admin)]
    [SwaggerOperation(Summary = "Gets all available product grocery stores", Description = "Available only for administrators")]
    public async Task<IActionResult> GetProductGroceryStores()
    {
        IEnumerable<ProductGroceryStoreDto> groceryStores = await _productGroceryStoreService.GetAllProductGroceryStores();
        return Ok(groceryStores);
    }

    [HttpPost]
    [Authorize(Roles = Role.Admin)]
    [SwaggerOperation(Summary = "Adds new product to grocery store",
        Description = "Available only for administrators")]
    public async Task<IActionResult> AddProductGroceryStore([FromBody] ProductGroceryStoreDto productGroceryStoreDto)
    {
        ProductGroceryStoreDto addedProductGroceryStore = await _productGroceryStoreService.AddProductGroceryStore(productGroceryStoreDto);
        return Ok(addedProductGroceryStore);
    }
}

