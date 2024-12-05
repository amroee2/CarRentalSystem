using CarRentalSystem.Models;

namespace CarRentalSystem.ViewModels
{
    public class CarListViewModel
    {

        public List<Car> Cars { get; set; }
        public CarListViewModel(List<Car> cars)
        {
            Cars = cars;
        }
    }
}
