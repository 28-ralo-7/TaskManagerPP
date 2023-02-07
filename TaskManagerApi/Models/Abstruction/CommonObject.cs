using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagerPP.Common.Models;

namespace TaskManagerApi.Models.Abstruction
{
    public class CommonObject
    {
        
        public string Name { get; set; }
        public string Discription { get; set; }
        public DateTime CreationDate { get; set; }
        public byte[] Photo { get; set; }


        public CommonObject()
        {
            CreationDate = DateTime.Now;
        }


        public CommonObject(CommonModel model)
        {
            Name = model.Name;
            Discription = model.Discription;
            CreationDate = model.CreationDate;
            Photo = model.Photo;
        }

    }
}
