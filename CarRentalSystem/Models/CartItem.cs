using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CarRentalSystem.Models
{
    public class CartItem
    {
        [BindNever]
        public int CartItemId { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        public DateTime RentalStart { get; set; }
        public DateTime RentalEnd { get; set; }
        public double Cost { get; set; }
        public int CartId { get; set; }
        public Cart Cart { get; set; }
    }
}
