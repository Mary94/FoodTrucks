namespace FoodTrucks.Domain.FoodTruck
{
    public interface IFoodTruckFinderService
    {
        List<FoodTruck> FilterFoodTrucks(IEnumerable<FoodTruck> foodTrucks, string searchTerm);
        IEnumerable<FoodTruck> FindFoodTrucks(IEnumerable<FoodTruck> foodTrucks, string searchTerm, double targetLatitude, double targetLongitude, int amount);
        public double CalculateDistance(double lat1, double lon1, double lat2, double lon2);
    }
}