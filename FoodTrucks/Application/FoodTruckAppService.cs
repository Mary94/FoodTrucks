using FindFoodTrucks.Services;
using FoodTrucks.Domain.FoodTruck;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace FoodTrucks.Application
{
    public class FoodTruckAppService : IFoodTruckAppService
    {
        private const string FoodTruckCacheKey = "FoodTruckData";
        private readonly int _cacheDuration;
        private readonly ApiService _apiService;
        private readonly IFoodTruckFinderService _foodTruckFinderService;
        private readonly IMemoryCache _cache;

        public FoodTruckAppService(ApiService apiService, IMemoryCache cache, IFoodTruckFinderService foodTruckFinderService,IConfiguration configuration)
        {
            _apiService = apiService;
            _cache = cache;
            _foodTruckFinderService = foodTruckFinderService;
            _cacheDuration = configuration.GetValue<int>("CacheDuration");
        }

        public async Task<IEnumerable<FoodTruck>> FindFoodTrucks(double targetLatitude, double targetLongitude, int amount, string searchTerm)
        {
            try
            {
                // Attempt to retrieve data from cache
                if (!_cache.TryGetValue(FoodTruckCacheKey, out IEnumerable<FoodTruck> foodTrucks))
                {
                    foodTrucks = await GetFoodTrucksFromApi();

                    // Cache the data for a specific duration
                    _cache.Set(FoodTruckCacheKey, foodTrucks, TimeSpan.FromMinutes(_cacheDuration));
                }

                return _foodTruckFinderService.FindFoodTrucks(foodTrucks, searchTerm, targetLatitude, targetLongitude, amount);
            }
            catch
            {
                throw;
            }
        }

        private async Task<List<FoodTruck>> GetFoodTrucksFromApi()
        {
            try
            {
                return await _apiService.GetJsonAsync<List<FoodTruck>>();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error fetching food trucks from API.", ex);
            }
        }
    }
}
