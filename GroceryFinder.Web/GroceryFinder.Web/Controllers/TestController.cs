using Microsoft.AspNetCore.Mvc;

namespace GroceryFinder.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    [HttpGet]
    [Route("test")]
    public IActionResult Test()
    {
        return Ok("Hello");
    }
}

