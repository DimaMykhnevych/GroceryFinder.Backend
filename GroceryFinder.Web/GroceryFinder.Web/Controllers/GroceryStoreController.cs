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

