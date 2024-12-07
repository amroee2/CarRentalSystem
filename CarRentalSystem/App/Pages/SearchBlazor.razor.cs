using CarRentalSystem.Models;
using CarRentalSystem.Models.Repositories;
using Microsoft.AspNetCore.Components;
using System.IO.Pipelines;

namespace CarRentalSystem.App.Pages
{
    public partial class SearchBlazor
    {
        public string SearchText = "";
        public List<Car> FilteredCars { get; set; } = new List<Car>();

        [Inject]
        public ICarRepository? CarRepository { get; set; }

        private void Search()
        {
            FilteredCars.Clear();
            if (CarRepository is not null)
            {
                if (SearchText.Length >= 3)
                    FilteredCars = CarRepository.SearchCars(SearchText).ToList();
            }
        }
    }
}
