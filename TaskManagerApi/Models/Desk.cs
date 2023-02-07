using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerApi.Models.Abstruction;

namespace TaskManagerApi.Models
{
    public class Desk:CommonObject
    {
        public int Id { get; set; }
        public bool IsPrivate { get; set; }
        public string Columns { get; set; }
        public User Admin { get; set; }
        public int AdminId { get; set; }
        public Project Projcet { get; set; }
        public int ProjcetId { get; set; }
        public List<Task> Tasks { get; set; } = new List<Task>();
    }
}
