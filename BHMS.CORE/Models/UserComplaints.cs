using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHMS.CORE.Models
{
    public class UserComplaints : BaseEntity
    {
        [Display(Name = "Student Email")]
        public string studentEmail { get; set; }

        [Display(Name = "Room Number")]
        [Required(ErrorMessage = "Please enter a valid room nnumber!")]
        public string RoomNo { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}
