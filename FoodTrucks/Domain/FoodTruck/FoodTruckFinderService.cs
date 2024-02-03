
namespace FoodTrucks.Domain.FoodTruck
{
    public class FoodTruckFinderService : IFoodTruckFinderService
    {
        public IEnumerable<FoodTruck> FindFoodTrucks(IEnumerable<FoodTruck> foodTrucks, string searchTerm, double targetLatitude, double targetLongitude, int amount)
        {
            var filteredFoodTrucks = FilterFoodTrucks(foodTrucks, searchTerm);

            // Sort food trucks by distance
            filteredFoodTrucks.Sort((truck1, truck2) =>
                CalculateDistance(truck1.Latitude, truck1.Longitude, targetLatitude, targetLongitude)
                .CompareTo(CalculateDistance(truck2.Latitude, truck2.Longitude, targetLatitude, targetLongitude)));

            return filteredFoodTrucks.Take(amount);
        }

        public List<FoodTruck> FilterFoodTrucks(IEnumerable<FoodTruck> foodTrucks, string searchTerm)
        {
            var filteredFoodTrucks = foodTrucks
                .Where(truck => truck.FoodItems != null && (truck.FoodItems.Equals("everything", StringComparison.OrdinalIgnoreCase) ||
                               (truck.FoodItems.Contains("All types of food", StringComparison.OrdinalIgnoreCase) && !truck.FoodItems.Contains("except for " + searchTerm, StringComparison.OrdinalIgnoreCase)) ||
                               (truck.FoodItems.Contains("everything", StringComparison.OrdinalIgnoreCase) && !truck.FoodItems.Contains("except for " + searchTerm, StringComparison.OrdinalIgnoreCase)) ||
                               (truck.FoodItems.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) && !truck.FoodItems.Contains("except for " + searchTerm, StringComparison.OrdinalIgnoreCase))))
                .ToList();
            return filteredFoodTrucks;
        }

        public double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            // Use Haversine formula for calculating distance between coordinates
            var R = 6371; // Earth radius in kilometers
            var dLat = ToRadians(lat2 - lat1);
            var dLon = ToRadians(lon2 - lon1);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            var distanceInKm = R * c;

            return distanceInKm;
        }

        private double ToRadians(double angle)
        {
            return angle * Math.PI / 180;
        }
    }
}
