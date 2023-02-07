using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagerPP.Common.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public byte[] Photo { get; set; }

        public UserStatus Status { get; set; }

        public UserModel()
        {

        }
        public UserModel(string fname, string lname, string email, string password, UserStatus status = UserStatus.User, string phone = null, byte[] photo = null)
        {
            FirstName = fname;
            LastName = lname;
            Email = email;
            Phone = phone;
            Password = password;
            Status = status;
            RegistrationDate = DateTime.Now;
        }
    }
}
