using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.InputModels
{
    public class UserRegistration : UserCredential
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
