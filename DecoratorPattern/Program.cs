using DecoratorPattern;
using DecoratorPattern.Entities;
using DecoratorPattern.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDecoratedScoped<IWeatherForecastService, ResilientWeatherForecastService, WeatherForecastService>(nameof(WeatherForecastService));

var app = builder.Build();

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
