using WeatherForecast.Infrastructure.Database.Entities;
using WeatherForecast.Infrastructure.Database.Repositories;

namespace WeatherForecast.Infrastructure.Database.Services;

internal class ForecastService(IRepository<Forecast> forecastRepository)
{
    public Task<List<Forecast>> GetForecasts(int skipCount, int limitCount) 
        => Task.FromResult(forecastRepository.GetAll()
            .Skip(skipCount)
            .Take(limitCount)
            .ToList());
}
