using System.ComponentModel.DataAnnotations;

namespace CarRentalSystem.Models
{
    public class Admin
    {
        public int AdminId { get; set; }
        [Required(ErrorMessage = "Please enter your first Name")]
        [Display(Name = "First Name")]
        [StringLength(20)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter your last Name")]
        [Display(Name = "Last Name")]
        [StringLength(20)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please enter a valid Email")]
        [Display(Name = "Email Address")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter a valid Password")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
