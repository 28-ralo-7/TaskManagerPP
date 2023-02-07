using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerPP.Common.Models
{
    public class CommonModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Discription { get; set; }
        public DateTime CreationDate { get; set; }
        public byte[] Photo { get; set; }

    }
}
