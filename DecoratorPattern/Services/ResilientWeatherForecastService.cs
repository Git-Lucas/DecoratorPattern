using DecoratorPattern.Entities;

namespace DecoratorPattern.Services;

public class ResilientWeatherForecastService(
    [FromKeyedServices(nameof(WeatherForecastService))] 
    IWeatherForecastService weatherForecastService) : IWeatherForecastService
{
    private readonly IWeatherForecastService _weatherForecastService = weatherForecastService;

    public WeatherForecast[] GetWeatherForecasts()
    {
        int retryCount = 0;
        
        Start:
        try
        {
            return _weatherForecastService.GetWeatherForecasts();
        }
        catch (Exception)
        {
            if (retryCount < 5)
            {
                retryCount++;
                goto Start;
            }

            throw;
        }
    }
}
