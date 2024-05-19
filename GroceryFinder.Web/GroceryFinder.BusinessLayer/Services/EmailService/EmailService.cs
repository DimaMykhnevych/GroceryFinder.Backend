using GroceryFinder.BusinessLayer.DTOs;
using GroceryFinder.BusinessLayer.Options;
using GroceryFinder.DataLayer.Models;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace GroceryFinder.BusinessLayer.Services.EmailService;

public class EmailService : IEmailService
{
    private readonly EmailServiceOptions _emailServiceDetails;

    public EmailService(IOptions<EmailServiceOptions> options)
    {
        _emailServiceDetails = options.Value;
    }

    public async Task SendAccountConfirmationEmail(AppUser user, string url)
    {
        MailAddress addressFrom = new (_emailServiceDetails.EmailAddress, "GroceryFinder");
        MailAddress addressTo = new (user.Email);
        MailMessage message = new (addressFrom, addressTo);

        message.Subject = "Підтвердження облікового запису";
        message.IsBodyHtml = true;
        string htmlString = @$"<html>
                      <body style='background-color: #f7f1d5; 
                        padding: 15px; border-radius: 15px; 
                        box-shadow: 5px 5px 15px 5px #9F9F9F;
                        font-size: 16px;'>
                      <p>Вітаємо, {user.UserName},</p>
                      <p>Будь ласка, підтвердьте свій обліковий запис, натиснувши наступне посилання.</p>
                      <a href={url}>Підтвердити обліковий запис</a>
                         <p>Дякуємо,<br>-GroceryFinder</br></p>
                      </body>
                      </html>
                     ";
        message.Body = htmlString;

        SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
        smtp.Credentials = new NetworkCredential(_emailServiceDetails.EmailAddress, _emailServiceDetails.Password);
        smtp.EnableSsl = true;
        await smtp.SendMailAsync(message);
    }

    public async Task SendPriceUpdateEmail(PriceUpdateEmailInfoDto priceUpdateEmailInfoDto)
    {
        MailAddress addressFrom = new(_emailServiceDetails.EmailAddress, "GroceryFinder");
        MailAddress addressTo = new(priceUpdateEmailInfoDto.Receiver.Email);
        MailMessage message = new(addressFrom, addressTo);

        message.Subject = "Оновлення вартості продукту";
        message.IsBodyHtml = true;
        string htmlString = @$"<html>
<body style='background-color: #f7f1d5; 
padding: 15px; border-radius: 15px; 
box-shadow: 5px 5px 15px 5px #9F9F9F;
font-size: 16px;'>
<p>Вітаємо, {priceUpdateEmailInfoDto.Receiver.UserName},</p>
<p>В магазині {priceUpdateEmailInfoDto.GroceryStore.Name} за адресою</p>
<img src='{priceUpdateEmailInfoDto.GroceryStore.LogoImageUri}' style='width: 30%; border-radius: 20px; float: right; margin-top: -60px'/>
<p><b>{priceUpdateEmailInfoDto.GroceryStore.Street}, {priceUpdateEmailInfoDto.GroceryStore.City}</b></p>
<p>змінилася ціна на продукт ""{priceUpdateEmailInfoDto.Product.Name}"".</p>
<img src='{priceUpdateEmailInfoDto.Product.ImageUri}' style='width: 200px; border-radius: 20px'/>
<p>Стара ціна - <b>{priceUpdateEmailInfoDto.OldPrice} грн</b>.</p>
<p>Нова ціна - <b>{priceUpdateEmailInfoDto.NewPrice} грн</b>.</p>
<p>Дякуємо,<br>-GroceryFinder</br></p>
</body>
</html>
                     ";
        message.Body = htmlString;

        SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
        smtp.Credentials = new NetworkCredential(_emailServiceDetails.EmailAddress, _emailServiceDetails.Password);
        smtp.EnableSsl = true;
        await smtp.SendMailAsync(message);
    }

    public async Task SendPrroductAddedEmail(ProductAddedEmailInfoDto productAddedEmailInfoDto)
    {
        MailAddress addressFrom = new(_emailServiceDetails.EmailAddress, "GroceryFinder");
        MailAddress addressTo = new(productAddedEmailInfoDto.Receiver.Email);
        MailMessage message = new(addressFrom, addressTo);

        message.Subject = "Новий продукт";
        message.IsBodyHtml = true;
        string htmlString = @$"<html>
<body style='background-color: #f7f1d5; 
padding: 15px; border-radius: 15px; 
box-shadow: 5px 5px 15px 5px #9F9F9F;
font-size: 16px;'>
<p>Вітаємо, {productAddedEmailInfoDto.Receiver.UserName},</p>
<p>В магазині {productAddedEmailInfoDto.GroceryStore.Name} за адресою</p>
<img src='{productAddedEmailInfoDto.GroceryStore.LogoImageUri}' style='width: 30%; border-radius: 20px; float: right; margin-top: -60px'/>
<p><b>{productAddedEmailInfoDto.GroceryStore.Street}, {productAddedEmailInfoDto.GroceryStore.City}</b></p>
<p>додався продукт ""{productAddedEmailInfoDto.Product.Name}"".</p>
<img src='{productAddedEmailInfoDto.Product.ImageUri}' style='width: 200px; border-radius: 20px'/>
<p>Ціна - <b>{productAddedEmailInfoDto.Price} грн</b>.</p>
<p>Дякуємо,<br>-GroceryFinder</br></p>
</body>
</html>
                     ";
        message.Body = htmlString;

        SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
        smtp.Credentials = new NetworkCredential(_emailServiceDetails.EmailAddress, _emailServiceDetails.Password);
        smtp.EnableSsl = true;
        await smtp.SendMailAsync(message);
    }
}

