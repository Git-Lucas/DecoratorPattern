using DecoratorPattern.Entities;
using DecoratorPattern.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddKeyedScoped<IWeatherForecastService, WeatherForecastService>(nameof(WeatherForecastService))
    .AddScoped<IWeatherForecastService, ResilientWeatherForecastService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/weatherforecast", (IWeatherForecastService weatherForecastService) =>
{
    WeatherForecast[] forecasts =  weatherForecastService.GetWeatherForecasts();
    
    return forecasts;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();
