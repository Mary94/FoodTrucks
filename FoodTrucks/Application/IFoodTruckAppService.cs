using FoodTrucks.Domain.FoodTruck;

namespace FoodTrucks.Application
{
    public interface IFoodTruckAppService
    {
        public Task<IEnumerable<FoodTruck>> FindFoodTrucks(double targetLatitude, double targetLongitude, int amount, string searchTerm);
    }
}
