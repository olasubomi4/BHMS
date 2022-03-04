using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;


namespace BHMS.CORE.Models
{
    public class Hostel : BaseEntity
    {

        public string Hostelname { get; set; }
        public int Capacity { get; set; }
        public int Availablespace { get; set; }
        public string Halladmin { get; set; }

      

       

        public string HostelImage { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public Gender Gender { get; set; }

        [Required]
        [Display(Name = "Category")]
        public HostelCategoryy HostelCategory { get; set; }

        public int Hostelblocks { get; set; }

        public int Roomsperblock { get; set; }

       


    }

    
    public enum Gender
    { Male = 0, Female = 1 }
    public enum HostelCategoryy
    { Classic = 0, Premiunm = 1 , Regular = 2}







}