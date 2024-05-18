using GroceryFinder.BusinessLayer.Constants;
using GroceryFinder.BusinessLayer.DTOs;
using GroceryFinder.BusinessLayer.Services.UserAllergyService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace GroceryFinder.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserAllergyController : ControllerBase
    {
        private readonly IUserAllergyService _userAllergyService;

        public UserAllergyController(IUserAllergyService userAllergyService)
        {
            _userAllergyService = userAllergyService;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Gets user allergy by id")]
        public async Task<IActionResult> GetUserAllergy(Guid id)
        {
            var allergy = await _userAllergyService.Get(id);
            return Ok(allergy);
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Gets all user allergies")]
        public async Task<IActionResult> GetUserAllergies()
        {
            var userId = new Guid(User.FindFirstValue(AuthorizationConstants.ID));
            var allergies = await _userAllergyService.GetAll(userId);
            return Ok(allergies);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Adds new user allergy")]
        public async Task<IActionResult> AddUserAllergy([FromBody] UserAllergyDto userAllergyDto)
        {
            var userId = new Guid(User.FindFirstValue(AuthorizationConstants.ID));
            UserAllergyDto addedUserAllergy = await _userAllergyService.Add(userId, userAllergyDto);
            return Ok(addedUserAllergy);
        }

        [HttpPut]
        [SwaggerOperation(Summary = "Updates user allergy")]
        public async Task<IActionResult> UpdateUserAllergy([FromBody] UserAllergyDto userAllergyDto)
        {
            var userId = new Guid(User.FindFirstValue(AuthorizationConstants.ID));
            UserAllergyDto updatedUserAllergy = await _userAllergyService.Update(userId, userAllergyDto);
            return Ok(updatedUserAllergy);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deletes user allergy by id")]
        public async Task<IActionResult> DeleteUserAllergy(Guid id)
        {
            await _userAllergyService.Delete(id);
            return Ok();
        }
    }
}
