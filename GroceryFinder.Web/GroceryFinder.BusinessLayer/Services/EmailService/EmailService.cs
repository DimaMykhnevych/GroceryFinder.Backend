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

        message.Subject = "Account Confirmation";
        message.IsBodyHtml = true;
        string htmlString = @$"<html>
                      <body style='background-color: #f7f1d5; 
                        padding: 15px; border-radius: 15px; 
                        box-shadow: 5px 5px 15px 5px #9F9F9F;
                        font-size: 16px;'>
                      <p>Hello {user.UserName},</p>
                      <p>Please, confirm your account by clicking the following link.</p>
                      <a href={url}>Confirm Account</a>
                         <p>Thank you,<br>-GroceryFinder</br></p>
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

