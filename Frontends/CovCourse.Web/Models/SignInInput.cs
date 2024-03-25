using System.ComponentModel.DataAnnotations;

namespace CovCourse.Web.Models
{
    public class SignInInput
    {
        [Required]
        [Display(Name ="Enter Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Enter Password")]
        public string Password { get; set; }
        [Display(Name = "Remember Me")]
        public bool IsRemember { get; set; }
    }
}
