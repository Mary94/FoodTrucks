using FoodTrucks.Domain.FoodTruck;
using Microsoft.Extensions.Hosting;

namespace FoodTrucks.Tests;

public class FoodTruckFinderServiceTest
{
    public static IEnumerable<object[]> PrefferedFood_Amount()
    {
        yield return new object[] { 37.7749 , -122.4294 , 2, "Tacos" };
        yield return new object[] { 37.7759, -122.4194, 0, "hot dogs" };
        yield return new object[] { 37.7849, -122.4193, 1, "pizza" };
    }

    public static IEnumerable<object[]> PrefferedFood_Found()
    {
        yield return new object[] { 37.7749, -122.4294, 2, "Tacos", true };
        yield return new object[] { 37.7759, -122.4194, 2, "hot dogs", false };
        yield return new object[] { 37.7849, -122.4193, 1, "pizza", true };
        yield return new object[] { 37.7849, -122.4193, 1, "everything except for hot dogs", true };
    }

    private static List<FoodTruck> GetFoodTrucks()
    {
        return new List<FoodTruck> {
                                     new FoodTruck { LocationId = 1656382, FoodItems = "Tacos: Tortas: Burritos", Latitude = 37.72980548057414, Longitude = -122.39924710472444 },
                                     new FoodTruck { LocationId = 1741557, FoodItems = "Hot coffee: iced coffee: hot chocolate: tea: pastries", Latitude = 37.79621549659414, Longitude = -122.40375455824538 },
                                     new FoodTruck { LocationId = 1757023, FoodItems = "everything except for hot dogs", Latitude = 37.77632714778992, Longitude = -122.39179682107691 },
                                     new FoodTruck { LocationId = 1744302, FoodItems = "Acai Bowls: Smoothies: Juices", Latitude = 37.79236678688307, Longitude = -122.40014830676716 },
                                     new FoodTruck { LocationId = 1735098, FoodItems = "Lobster rolls: crab rolls: lobster burritos: crab burritos: chicken burritos: fish burritos: chicken burritos: poke bowls: soups: chips & soda.", Latitude = 37.793262206923096, Longitude = -122.4017890913628 }};
    }

    [Theory]
    [MemberData(nameof(PrefferedFood_Found))]
    public void FindFoodTrucks_ShouldMatchExpectedValue(double latitude, double longitude, int amount, string preferredFood, bool expectedValue)
    {
        var foodTrucks = GetFoodTrucks();
        var finderService = new FoodTruckFinderService();

        IEnumerable<FoodTruck> result = finderService.FindFoodTrucks(foodTrucks, preferredFood, latitude, longitude, amount);

        Assert.Equal(result.Any(), expectedValue);
    }

    [Theory]
    [MemberData(nameof(PrefferedFood_Amount))]
    public void FindFoodTrucks_ShouldMatchExpectedAmount(double latitude, double longitude, int amount, string preferredFood)
    {
        var foodTrucks = GetFoodTrucks();
        var finderService = new FoodTruckFinderService();

        IEnumerable<FoodTruck> result = finderService.FindFoodTrucks(foodTrucks, preferredFood, latitude, longitude, amount);

        Assert.Equal(result.Count(), amount);
    }
}