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
        [Display(Name = "Hostel Name")]
        public string Hostelname { get; set; }

        [Display(Name = "Hostel Capacity")]
        public int Capacity { get; set; }
        [Display(Name = "Available Space")]
        public int Availablespace { get; set; }

        [Display(Name = "Hall Admin ")]
        public string Halladmin { get; set; }




        [Display(Name = "Hostel Image")]
        public string HostelImage { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public Gender Gender { get; set; }

        [Required]
        [Display(Name = "Category")]
        public HostelCategoryy HostelCategory { get; set; }

        [Display(Name = "Number of Blocks")]
        public int Hostelblocks { get; set; }

        [Display(Name = "Number of rooms per block")]
        public int Roomsperblock { get; set; }

        [Display(Name = "Rooms")]
        public string rooms { get; set; }

    }

    
    public enum Gender
    { Male = 0, Female = 1 }
    public enum HostelCategoryy
    { Classic = 0, Premiunm = 1 , Regular = 2}







}