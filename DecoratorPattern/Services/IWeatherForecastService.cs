using DecoratorPattern.Entities;

namespace DecoratorPattern.Services;

public interface IWeatherForecastService
{
    WeatherForecast[] GetWeatherForecasts();
}
