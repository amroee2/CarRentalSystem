using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CarRentalSystem.Models
{
    public class User : IdentityUser
    {
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
        [Required(ErrorMessage = "Please enter your Phone Number")]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Please enter your Address")]
        [Display(Name = "Primary Address")]
        public string PrimaryAddress { get; set; }
        [Display(Name = "Secondary Address")]
        public string SecondaryAddress { get; set; }
        [Required(ErrorMessage = "Please enter your City")]
        [Display(Name = "City")]
        public string City { get; set; }
        [Required(ErrorMessage = "Please enter your Country")]
        [Display(Name = "Country")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Please enter your Driver License Number")]
        [Display(Name = "Drivers License Number")]
        public string DriversLicenseNumber { get; set; }

        public List<Rental> Rentals { get; set; }

        public User()
        {
            Rentals = new List<Rental>();
        }
    }
}
