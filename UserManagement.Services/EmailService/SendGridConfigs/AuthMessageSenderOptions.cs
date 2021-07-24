namespace UserManagement.Services.EmailService.SendGridConfigs
{
    /// <summary>
    /// these configs needs to be initialized with user-secrets
    /// ref: https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-5.0&tabs=windows
    /// </summary>
    public class AuthMessageSenderOptions
    {
        public string SendGridUser { get; set; }
        public string SendGridKey { get; set; }
    }
}
