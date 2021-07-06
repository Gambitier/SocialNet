namespace UserManagement.Services.Services.RequestModels
{
    public class UserRegistration : UserCredential
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
