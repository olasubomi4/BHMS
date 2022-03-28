using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHMS.CORE.Models
{
    public class ItemCategory : BaseEntity
    { 
        [Required]
        [Display(Name = "Item Category")]
        public string Category { get; set; }

    }
}
