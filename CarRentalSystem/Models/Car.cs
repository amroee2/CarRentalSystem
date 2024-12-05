using System.ComponentModel.DataAnnotations;

namespace CarRentalSystem.Models
{
    public class Car
    {
        [Key]
        public int CarId { get; set; }
        [Required(ErrorMessage = "Please enter a Car Name")]
        [Display(Name = "Car Name")]
        public string CarName { get; set; }
        [Required(ErrorMessage = "Please enter a Car Model")]
        [Display(Name = "Car Model")]
        public string CarModel { get; set; }
        [Required(ErrorMessage = "Please enter a Car Image")]
        [Display(Name = "Car Image")]
        public string CarImage { get; set; }
        [Required(ErrorMessage = "Please enter a Car Description")]
        public string CarDescription { get; set; }
        [Required(ErrorMessage = "Please enter a Car Price")]
        public double RentPricePerDay { get; set; }
        [Required(ErrorMessage = "Please enter Car Availability")]
        public bool IsAvailable { get; set; }

        public List<Rental> Rentals { get; set; }

        public Car()
        {
            Rentals = new List<Rental>();
        }
    }
}
