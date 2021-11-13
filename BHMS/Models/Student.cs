using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BHMS.Models
{
    public class Student
    {
        [Required]
        [Display(Name ="Matric Number")]
        [Range(1000,9999, ErrorMessage ="Enter a Valid Matric Number!")]
        public string MatricNo { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "You need to give us your First name!")]
        public string FullName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "You need to give us your Last name!")]
        public string LastName { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Course of Study")]
        public string CourseOfStudy { get; set; }

        [DataType(DataType.EmailAddress)] //validates to make sure its a valid email address
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Enter a valid email address!")]
        public string EmailAddress { get; set; }

        [Display(Name = "Confirm Email")]
        [Compare("EmailAddress", ErrorMessage = "The email and confirm email must match!")]
        public string ConfirmEmail { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "You need to provide a long enough password!")]
        [Required(ErrorMessage = "You must have a password!")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Your password and confirm password do not match!")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "You need to enter your LEVEL!")]
        [Range(100,900, ErrorMessage = "Enter a valid Level!")]
        [Display(Name = "Level")]
        public int Level { get; set; }

        [Display(Name = "Nationality")]
        public string Nationality { get; set; }

        [Required(ErrorMessage = "Enter a valid Sex!")]
        [Display(Name = "Sex")]
        public bool Sex { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public string DOB { get; set; }
    }
}