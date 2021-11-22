using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHMS.CORE.Models
{
    public  class Item:BaseEntity
    {
        public string Color { get; set; }
        public string SerialNumber { get; set; }
        public string Image { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }



    }
}
