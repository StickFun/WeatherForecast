using WeatherForecast.Infrastructure.Database.Entities;
using WeatherForecast.Infrastructure.Database.Repositories;

namespace WeatherForecast.Infrastructure.Database.Services;

internal class ForecastService(IRepository<Forecast> forecastRepository) : IForecastService
{
    public Task<List<Forecast>> GetForecasts(int skip, int take)
        => Task.FromResult(forecastRepository.GetAll()
            .Skip(skip)
            .Take(take)
            .ToList());
}
