using GroceryFinder.BusinessLayer.Constants;
using GroceryFinder.BusinessLayer.DTOs;
using GroceryFinder.BusinessLayer.Exceptions;
using GroceryFinder.BusinessLayer.Services.UserService;
using GroceryFinder.DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace GroceryFinder.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _service;
    public UserController(IUserService service)
    {
        _service = service;
    }

    [HttpGet("{username}")]
    public async Task<IActionResult> Get(string username)
    {
        return Ok(await _service.GetUserByUsername(username));
    }

    [HttpPost]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "Registers a new user",
            Description = "'role' property can be ignored, by default all users have 'User' role\n\n" +
            "clientURIForEmailConfirmation - the base URI to the page where email confirmation performs, e.g. http://localhost:4200/emailConfirmation")]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(bool))]
    [SwaggerResponse((int)HttpStatusCode.Conflict, Description = "Provided username has already been taken. See details in the error response")]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Provided passwords do not match. See details in the error response")]
    [SwaggerResponse((int)HttpStatusCode.UnprocessableEntity, Description = "Other validation errors. See details in the error response")]
    public async Task<IActionResult> Post([FromBody] CreateUserDto model)
    {
        model.Role = "User";
        try
        {
            return Ok(await _service.CreateUserAsync(model));
        }
        catch (UsernameAlreadyTakenException)
        {
            return BadRequest(AddModelStateError("username", ErrorMessagesConstants.USERNAME_ALREADY_TAKEN));
        }
    }

    [HttpPost("confirmEmail")]
    [SwaggerOperation(Summary = "Performs confirmation of the email based on the given token",
            Description = "In the 'Development' mode validation of the email is disabled")]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(bool))]
    [SwaggerResponse((int)HttpStatusCode.UnprocessableEntity, Description = "Token validation errors, e.g. invalid token provided. See details in the error response")]
    [SwaggerResponse((int)HttpStatusCode.NotFound, Description = "User with provided email was not found")]
    public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailDto confirmEmailDto)
    {
        ConfirmEmailDto confirmEmail = await _service.ConfirmEmail(confirmEmailDto);
        if (confirmEmail == null)
            return BadRequest("Invalid Email Confirmation Request");
        return Ok(confirmEmail);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = Role.Admin)]
    [SwaggerOperation(Summary = "Deletes user by Id",
            Description = "Available only for administrators")]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(bool))]
    [SwaggerResponse((int)HttpStatusCode.UnprocessableEntity, Description = "Errors occurred during user deletion. See details in the error response")]
    [SwaggerResponse((int)HttpStatusCode.NotFound, Description = "User with provided Id was not found")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, Description = "User was not authorized")]
    [SwaggerResponse((int)HttpStatusCode.Forbidden, Description = "User is not administrator")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteUser(id);
        return Ok();
    }

    private ModelStateDictionary AddModelStateError(string field, string error)
    {
        ModelStateDictionary modelState = new ();
        modelState.TryAddModelError(field, error);
        return modelState;
    }
}

