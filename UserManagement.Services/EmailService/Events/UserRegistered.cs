using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;
using UserManagement.Services.EmailService.EmailEventArgs;

namespace UserManagement.Services.EmailService.Events
{
    public class UserRegistered
    {
        private readonly IEmailSender _emailSender;

        public UserRegistered(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public void UserRegisteredEvent(object sender, UserRegisteredEventArgs e)
        {
            var user = e.RegisteredUserData;

            // todo : can we do this method async, so we can await here?
            Task.Run(async () =>
            {
                await _emailSender.SendEmailAsync(
                    user.Email,
                    "welcome to usermanagement",
                    $"Welcome!! <br/><br/> " +
                    $"your username is \"{user.UserName.Trim().ToLower()}\" " +
                    $"and password is \"{user.Password}\"");
            });
        }
    }
}
