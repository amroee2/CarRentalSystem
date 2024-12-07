using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CarRentalSystem.Models;
using CarRentalSystem.Models.Repositories;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'CarRentalSystemDbContextConnection' not found.");

builder.Services.AddDbContext<CarRentalSystemDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<CarRentalSystemDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<CarRentalSystemDbContext, CarRentalSystemDbContext>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IRentalRepository, RentalRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
async Task SeedAdminRoleAsync(IServiceProvider serviceProvider)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }
}
async Task SeedAdminUserAsync(IServiceProvider serviceProvider)
{
    var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

    string adminEmail = "admin2@gmail.com";
    string adminPassword = "Admin@12345";
    var existingUser = await userManager.FindByEmailAsync(adminEmail);

    if (existingUser == null)
    {
        var adminUser = new User
        {
            UserName = adminEmail,
            Email = adminEmail,
            FirstName = "Admin",
            LastName = "User",
            PrimaryAddress = "123 Admin St.",
            City = "AdminCity",
            Country = "AdminLand",
            DriversLicenseNumber = "ADMIN-123456",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            PhoneNumber = "1234567890"
        };

        var result = await userManager.CreateAsync(adminUser, adminPassword);

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
        else
        {
            throw new Exception("Failed to create the admin user: " + string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }
}

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    await SeedAdminRoleAsync(services);
    await SeedAdminUserAsync(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

