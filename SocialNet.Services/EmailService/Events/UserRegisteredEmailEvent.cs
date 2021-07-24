using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;
using SocialNet.Services.EmailService.EmailEventArgs;

namespace SocialNet.Services.EmailService.Events
{
    public class UserRegisteredEmailEvent : ConfigEmailEvent
    {
        public UserRegisteredEmailEvent(IEmailSender emailSender) 
            : base(emailSender) {}

        public void SendEmail(object sender, UserRegisteredEventArgs e)
        {
            var user = e.RegisteredUserData;

            // todo : can we do this method async, so we can await here?
            Task.Run(async () =>
            {
                await _emailSender.SendEmailAsync(
                    user.Email,
                    "welcome to SocialNet",
                    $"Welcome!! <br/><br/> " +
                    $"your username is \"{user.UserName.Trim().ToLower()}\" " +
                    $"and password is \"{user.Password}\"");
            });
        }
    }
}
