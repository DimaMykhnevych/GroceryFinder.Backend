using GroceryFinder.BusinessLayer.Constants;
using GroceryFinder.BusinessLayer.DTOs;
using GroceryFinder.BusinessLayer.Services.PriceUpdateSubscriptionService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace GroceryFinder.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PriceUpdateSubscriptionController : ControllerBase
    {
        private readonly IPriceUpdateSubscriptionService _priceUpdateSubscriptionService;

        public PriceUpdateSubscriptionController(IPriceUpdateSubscriptionService priceUpdateSubscriptionService)
        {
            _priceUpdateSubscriptionService = priceUpdateSubscriptionService;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Gets user product price update subscription by id")]
        public async Task<IActionResult> GetUserSubscription(Guid id)
        {
            var subscription = await _priceUpdateSubscriptionService.Get(id);
            return Ok(subscription);
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Gets all user product price update subscriptions")]
        public async Task<IActionResult> GetAllUserSubscriptions()
        {
            var userId = new Guid(User.FindFirstValue(AuthorizationConstants.ID));
            var subscriptions = await _priceUpdateSubscriptionService.GetAll(userId);
            return Ok(subscriptions);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Adds new user product price update subscription")]
        public async Task<IActionResult> AddUserSubscription([FromBody] AddPriceUpdateSubscriptionDto addPriceUpdateSubscriptionDto)
        {
            var userId = new Guid(User.FindFirstValue(AuthorizationConstants.ID));
            var addedSubscription = await _priceUpdateSubscriptionService.Add(userId, addPriceUpdateSubscriptionDto);
            return Ok(addedSubscription);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deletes user product price update subscription by id")]
        public async Task<IActionResult> DeleteUserSubscription(Guid id)
        {
            await _priceUpdateSubscriptionService.Delete(id);
            return Ok();
        }
    }
}
