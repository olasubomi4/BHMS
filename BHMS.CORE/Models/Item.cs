using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHMS.CORE.Models
{
    public  class Item:BaseEntity
    {
        [Display(Name = "Item Color")]
        public string Color { get; set; }
        [Display(Name = "Item Serial Number")]
        public string SerialNumber { get; set; }
        [Display(Name = "Item Image")]
        public string Image { get; set; }
        [Display(Name = "Item Category")]
        public string Category { get; set; }
        [Display(Name = "Item Name")]
        public string Name { get; set; }

        [Display(Name = "Item user")]
        public string user { get; set; }





    }
}
