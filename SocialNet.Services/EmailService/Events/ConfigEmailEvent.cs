using Microsoft.AspNetCore.Identity.UI.Services;

namespace SocialNet.Services.EmailService.Events
{
    public abstract class ConfigEmailEvent
    {
        protected readonly IEmailSender _emailSender;

        public ConfigEmailEvent(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }
    }
}