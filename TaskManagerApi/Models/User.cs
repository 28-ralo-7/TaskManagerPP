using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerPP.Common.Models;

namespace TaskManagerApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string  LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public byte[] Photo { get; set; }
        public UserStatus Status { get; set; }

        public List<Project> Projects { get; set; } = new List<Project>();
        public List<Desk> Desks { get; set; } = new List<Desk>();
        public List<Task> Tasks { get; set; } = new List<Task>();

        public User() { }
        public User(string fname,string lname,string email,string password, UserStatus status = UserStatus.User,string phone = null,byte[] photo = null)
        {
            FirstName = fname;
            LastName = lname;
            Email = email;
            Phone = phone;
            Password = password;
            Photo = photo;
            Status = status;
            RegistrationDate = DateTime.Now;
        }


        public User(UserModel model) 
        {
            FirstName = model.FirstName;
            LastName = model.LastName;
            Email = model.Email;
            Phone = model.Phone;
            Password = model.Password;
            Photo = model.Photo;
            Status = model.Status;
            RegistrationDate = model.RegistrationDate;
        }


        public UserModel ToDto()
        {
            return new UserModel()
            {
                Id = this.Id,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Email = this.Email,
                Phone = this.Phone,
                Password = this.Password,
                Photo = this.Photo,
                Status = this.Status,
                RegistrationDate = this.RegistrationDate
        };
        }
    }
}
