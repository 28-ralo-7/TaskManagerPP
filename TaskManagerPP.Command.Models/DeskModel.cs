using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerPP.Common.Models
{
    public class DeskModel:CommonModel
    {
        public bool IsPrivate { get; set; }
        public string[] Columns { get; set; }
        public int ProjcetId { get; set; }
        public List<TaskModel> Tasks { get; set; } = new List<TaskModel>();
        public int AdminId { get; set; }
    }
}
