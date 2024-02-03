using FindFoodTrucks.Services;
using FoodTrucks.Application;
using FoodTrucks.Domain.FoodTruck;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<ApiService>();

builder.Services.AddMemoryCache();
builder.Services.AddScoped<IFoodTruckFinderService, FoodTruckFinderService>();
builder.Services.AddScoped<IFoodTruckAppService, FoodTruckAppService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
