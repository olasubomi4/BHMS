using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using BHMS.CORE.Contract;
using FluentValidation;
using System.Text;
using System.Threading.Tasks;

namespace BHMS.CORE.Models
{
    public class HostelRegistration : BaseEntity
    {

        [Display(Name = "Hostel Identification Number")]
        public string Hostelid{ get; set; }
        [Display(Name = "User")]
        public string User { get; set; }
        [Display(Name = "Hall Admin")]
        public string Halladmin { get; set; }

        [Display(Name = "Rooms")]
        public string room { get; set; } 

        [StringLength(7, ErrorMessage = "Your matric number must be 7 letters long.", MinimumLength = 7), Display(Name = "Room mates matric number")]
       
        public string roommateNo{ get; set; }


      





    }
  
}