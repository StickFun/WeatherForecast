using WeatherForecast.Domain.Abstractions;

namespace WeatherForecast.Application.Common;

public class WeatherForecastContext : IWeatherForecastContext
{
    public string DatabaseConnectionString { get; set; }

    public string CurrentDataFileDirectoryPath { get; set; }
}
