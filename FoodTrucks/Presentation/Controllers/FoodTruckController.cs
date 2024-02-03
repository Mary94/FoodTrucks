using FindFoodTrucks.Services;
using FoodTrucks.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace FoodTrucks.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FoodTruckController : ControllerBase
    {
        private readonly IFoodTruckAppService _foodTruckAppService;

        public FoodTruckController(ILogger<FoodTruckController> logger, ApiService apiService, IMemoryCache cache, IFoodTruckAppService foodTruckAppService)
        {
            _foodTruckAppService = foodTruckAppService;
        }

        [HttpGet(Name = "FindFoodTrucks")]
        public async Task<IActionResult> GetAsync(double latitude, double longitude, int amount, string preferredFood)
        {
            try
            {
                var foodTrucks = await _foodTruckAppService.FindFoodTrucks(latitude, longitude, amount, preferredFood);

                return Ok(foodTrucks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
