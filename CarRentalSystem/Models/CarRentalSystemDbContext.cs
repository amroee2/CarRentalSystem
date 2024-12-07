using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystem.Models
{
    public class CarRentalSystemDbContext : IdentityDbContext<User>
    {
        public CarRentalSystemDbContext(DbContextOptions<CarRentalSystemDbContext> options) : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
            modelBuilder.Entity<Admin>()
                .HasIndex(u => u.Email)
                .IsUnique();
            modelBuilder.Entity<Cart>()
                .HasMany(c => c.CartItems)
                .WithOne(ci => ci.Cart)
                .HasForeignKey(ci => ci.CartId);

            modelBuilder.Entity<Car>().HasData(
                new Car { CarId = 1, CarName = "Toyota Corolla", CarModel = "2020", CarImage = "https://carstreetindia.com/blogs/wp-content/uploads/2022/09/25-1024x683.jpg", CarDescription = "Compact car, fuel-efficient", RentPricePerDay = 50, IsAvailable = true },
                new Car { CarId = 2, CarName = "Honda Civic", CarModel = "2021", CarImage = "https://carstreetindia.com/blogs/wp-content/uploads/2022/09/25-1024x683.jpg", CarDescription = "Compact car, sporty design", RentPricePerDay = 55, IsAvailable = true },
                new Car { CarId = 3, CarName = "Ford Focus", CarModel = "2019", CarImage = "https://carstreetindia.com/blogs/wp-content/uploads/2022/09/25-1024x683.jpg", CarDescription = "Reliable car, comfortable ride", RentPricePerDay = 45, IsAvailable = true },
                new Car { CarId = 4, CarName = "Chevrolet Malibu", CarModel = "2022", CarImage = "https://carstreetindia.com/blogs/wp-content/uploads/2022/09/25-1024x683.jpg", CarDescription = "Mid-size car, modern features", RentPricePerDay = 60, IsAvailable = true },
                new Car { CarId = 5, CarName = "Hyundai Elantra", CarModel = "2023", CarImage = "https://carstreetindia.com/blogs/wp-content/uploads/2022/09/25-1024x683.jpg", CarDescription = "Affordable car, great fuel economy", RentPricePerDay = 48, IsAvailable = true },
                new Car { CarId = 6, CarName = "Nissan Altima", CarModel = "2020", CarImage = "https://carstreetindia.com/blogs/wp-content/uploads/2022/09/25-1024x683.jpg", CarDescription = "Mid-size sedan, comfortable seating", RentPricePerDay = 52, IsAvailable = true },
                new Car { CarId = 7, CarName = "BMW 3 Series", CarModel = "2021", CarImage = "https://carstreetindia.com/blogs/wp-content/uploads/2022/09/25-1024x683.jpg", CarDescription = "Luxury sedan, powerful engine", RentPricePerDay = 150, IsAvailable = true },
                new Car { CarId = 8, CarName = "Mercedes-Benz C-Class", CarModel = "2019", CarImage = "https://carstreetindia.com/blogs/wp-content/uploads/2022/09/25-1024x683.jpg", CarDescription = "Luxury car, elegant design", RentPricePerDay = 180, IsAvailable = true },
                new Car { CarId = 9, CarName = "Audi A4", CarModel = "2022", CarImage = "https://carstreetindia.com/blogs/wp-content/uploads/2022/09/25-1024x683.jpg", CarDescription = "High-performance car, premium features", RentPricePerDay = 160, IsAvailable = true },
                new Car { CarId = 10, CarName = "Kia Soul", CarModel = "2023", CarImage = "https://carstreetindia.com/blogs/wp-content/uploads/2022/09/25-1024x683.jpg", CarDescription = "Compact SUV, fun to drive", RentPricePerDay = 70, IsAvailable = true }
            );
        }
    }
}
