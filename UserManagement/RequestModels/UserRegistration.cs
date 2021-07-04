namespace UserManagement.RequestModels
{
    public class UserRegistration : UserCredential
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
