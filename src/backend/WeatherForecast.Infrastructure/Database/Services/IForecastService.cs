using WeatherForecast.Infrastructure.Database.Entities;

namespace WeatherForecast.Infrastructure.Database.Services;

public interface IForecastService
{
    Task<List<Forecast>> GetForecasts(int skipCount, int limitCount);
}