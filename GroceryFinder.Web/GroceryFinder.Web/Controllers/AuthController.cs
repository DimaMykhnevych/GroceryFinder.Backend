using GroceryFinder.BusinessLayer.Services.AuthorizationService;
using GroceryFinder.DataLayer.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace GroceryFinder.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly BaseAuthorizationService _authorizationService;
    public AuthController(BaseAuthorizationService authorizationService)
    {
        _authorizationService = authorizationService;
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("token")]
    [SwaggerOperation(Summary = "Gets a Bearer token with basic user info in case of successful authorization",
            Description = "As a result returns the model with LoginErrorCode property.\n\nPossible LoginErrorCode values:\n\n" +
            "0 = Invalid userName or password\n\n" +
            "1 = Email confirmation required\n\n" +
            "100 = User was successfully authorized")]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(JWTTokenStatusResult))]
    public async Task<IActionResult> Login([FromBody] AuthSignInModel model)
    {
        JWTTokenStatusResult result = await _authorizationService.GenerateTokenAsync(model);
        return Ok(result);
    }

    [HttpGet]
    [Route("current-user-info")]
    [SwaggerOperation(Summary = "Gets current logged in user info")]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(UserAuthInfo))]
    [SwaggerResponse((int)HttpStatusCode.NotFound, Description = "User was not found")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, Description = "User was not authorized")]
    public async Task<IActionResult> GetUserInfo()
    {
        string currentUserName = User.Identity.Name;
        UserAuthInfo userInfo = await _authorizationService.GetUserInfoAsync(currentUserName);

        if (userInfo == null)
        {
            return NotFound();
        }

        return Ok(userInfo);
    }
}

