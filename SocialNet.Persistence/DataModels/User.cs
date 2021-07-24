using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNet.Persistence.DataModels
{
    [Table("Users")]
    public class User : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
