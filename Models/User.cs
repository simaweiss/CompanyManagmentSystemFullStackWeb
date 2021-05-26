using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagmentSystem.Models
{

    public class User
    {
        public string UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string DateOfBirth { get; set; }
        public string CompanyName { get; set; }
        public string CompanyID { get; set; }
        public string Password { get; set; }
        public string LastLoginDate { get; set; }
        public string Email { get; set; }
    }
}
