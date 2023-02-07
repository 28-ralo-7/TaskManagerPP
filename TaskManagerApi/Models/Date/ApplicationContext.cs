using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerPP.Common.Models;

namespace TaskManagerApi.Models.Date
{
    public class ApplicationContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<ProjectAdmin> ProjectAdmins { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Desk> Desks { get; set; }
        public DbSet<Task> Tasks { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
            if (this.Users.Any(u => u.Status == UserStatus.Admin) == false)
            {
                var admin = new User("alexander","frolov","pochta","qwerty",UserStatus.Admin);
                Users.Add(admin);
                SaveChanges();
            }
        }
    }
}
