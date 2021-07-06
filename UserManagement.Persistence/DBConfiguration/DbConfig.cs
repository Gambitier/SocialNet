namespace UserManagement.Persistence.DBConfiguration
{
    /// <summary>
    /// these configs needs to be initialized with user-secrets
    /// ref: https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-5.0&tabs=windows
    /// </summary>
    public class DbConfig
    {
        public string Database_Name { get; set; }
        public string Connection_String { get; set; }
        public string User_Collection_Name { get; set; }
    }
}
