using BHMS.CORE.Models;
using BHMS.SQL;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FluentValidation;
using System.Web.Mvc;
using CompareAttribute = System.ComponentModel.DataAnnotations.CompareAttribute;


namespace BHMS.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
   

    public class RegisterViewModel : BaseEntity
    {
        public string Name { get; set; }

        [Required]
        [EmailAddress] 
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required, Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Required, Display(Name = "Lastname")]
        public string LastName { get; set; }

        [Required]
        [StringLength(7, ErrorMessage = "Your matric number must be 7 letters long.", MinimumLength = 7)]
     
        [Display(Name = "Matric Number")]
        [EmailValidationN(ErrorMessage = "The Matric no already exists")]
        
        public string MatricNo { get; set; }

        [Required, Display(Name = "Course")]
        public string Course { get; set; }

        [Required, Display(Name = "Level")]
        public int Level { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public Gender Gender { get; set; }


    }
    public enum Gender
    { Male = 0, Female = 1 }

   
   /* public class PlaceValidator : AbstractValidator<RegisterViewModel>
    {
        public PlaceValidator()
        {
            
            RuleFor(x => x.MatricNo).Must(BeUniqueUrl).WithMessage("MATRIC NUMBER already exists");
        }

        private bool BeUniqueUrl(string MatricNo)
        {
            return new ApplicationDbContext().Users.FirstOrDefault(x => x.MatricNo == MatricNo) == null;
        }
    }
   */
    public class EmailValidationN : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            
            if (value != null)
            {
                var valueAsString = value.ToString();
                IEnumerable<string> matricNo = new ApplicationDbContext().Users.Where(x => x.MatricNo != null).Select(x => x.MatricNo); 
        
                if (matricNo.Contains(valueAsString))
                {
                    var errorMessage = FormatErrorMessage(validationContext.DisplayName);
                    return new ValidationResult(errorMessage);
                }
                
            }
            return ValidationResult.Success;
        }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
