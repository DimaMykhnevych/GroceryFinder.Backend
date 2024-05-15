using GroceryFinder.BusinessLayer.DTOs;
using GroceryFinder.BusinessLayer.Services.GroceryStoreService;
using GroceryFinder.DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GroceryFinder.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GroceryStoreController : ControllerBase
{
    private readonly IGroceryStoreService _groceryStoreService;
    public GroceryStoreController(IGroceryStoreService groceryStoreService)
    {
        _groceryStoreService = groceryStoreService;
    }

    [HttpGet("get-all")]
    [Authorize(Roles = Role.Admin)]
    [SwaggerOperation(Summary = "Gets all available grocery stores", Description = "Available only for administrators")]
    public async Task<IActionResult> GetGroceryStores()
    {
        IEnumerable<GroceryStoreDto> groceryStores = await _groceryStoreService.GetAllGroceryStores();
        return Ok(groceryStores);
    }

    [HttpGet("search-by-product")]
    [SwaggerOperation(
        Summary = "Searches grocery stores that contain certain product",
        Description = "The ProductId is mandatory and should be set for all search modes.\n\n" +
        "Latitude, Longitude - the current location of the user (can be retrieved from the browser).\n\n" +
        "Radius should be set in meters.\n\n" +
        "The following search approaches are supported:\n" +
        "1. Find stores with required product in certain radius. For that mode Latitude, Longitude and Radius should be set.\n" +
        "2. Find nearest stores with required product (top 10 nearest stores). For that mode Latitude, Longitude and GroceryStoreSearchMode = 1 should be set.\n" +
        "3. Find stores with the lowest prices for required product (top 10 cheapest stores). For that mode GroceryStoreSearchMode = 2 should be set.\n\n" +
        "You can combine these three search modes, e.g. you can find top 10 cheapest stores in certain radius, etc.\n\n" +
        "Also you can set just ProductId and then all stores that have that product will be displayed.")]
    public async Task<IActionResult> SearchGroceryStores([FromQuery] GroceryStoreSearchDto groceryStoreSearchDto)
    {
        var stores = await _groceryStoreService.SearchGroceryStores(groceryStoreSearchDto);
        return Ok(stores);
    }

    [HttpPost]
    [Authorize(Roles = Role.Admin)]
    [SwaggerOperation(Summary = "Adds new grocery store",
        Description = "Available only for administrators")]
    public async Task<IActionResult> AddGroceryStore([FromBody] GroceryStoreDto groceryStoreDto)
    {
        GroceryStoreDto addedGroceryStore = await _groceryStoreService.AddGroceryStore(groceryStoreDto);
        return Ok(addedGroceryStore);
    }
}

