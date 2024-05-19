using GroceryFinder.BusinessLayer.DTOs;
using GroceryFinder.DataLayer.Models;

namespace GroceryFinder.BusinessLayer.Services.EmailService;

public interface IEmailService
{
    Task SendAccountConfirmationEmail(AppUser receiver, string url);
    Task SendPriceUpdateEmail(PriceUpdateEmailInfoDto priceUpdateEmailInfoDto);
    Task SendPrroductAddedEmail(ProductAddedEmailInfoDto productAddedEmailInfoDto);
}

