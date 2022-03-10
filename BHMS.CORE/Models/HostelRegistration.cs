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

        public string Hostelid{ get; set; }
        public string User { get; set; }
        public string Halladmin { get; set; }

        public string room { get; set; } 

        [StringLength(7, ErrorMessage = "Your matric number must be 7 letters long.", MinimumLength = 7), Display(Name = "Room mates matric number")]
        public string roommateNo{ get; set; }


      





    }
  
}