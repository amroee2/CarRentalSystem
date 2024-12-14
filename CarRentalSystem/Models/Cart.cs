namespace CarRentalSystem.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public List<CartItem> CartItems { get; set; }
        public double TotalCost { get; set; }
        public bool IsCheckedOut { get; set; }
        public DateTime CheckedOutDate { get; set; }
        public Cart()
        {
            CartItems = new List<CartItem>();
        }
    }
}
