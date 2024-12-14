namespace CarRentalSystem.Models
{
    public class Rental
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        public DateTime RentalStartDate { get; set; }
        public DateTime RentalEndDate { get; set; }
        public double TotalCost { get; set; }
    }
}
