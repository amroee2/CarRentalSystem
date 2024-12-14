using CarRentalSystem.Models;
using Microsoft.AspNetCore.Components;

namespace CarRentalSystem.App.Pages
{
    public partial class CarCard
    {
        [Parameter]
        public Car? Car { get; set; }
    }
}
