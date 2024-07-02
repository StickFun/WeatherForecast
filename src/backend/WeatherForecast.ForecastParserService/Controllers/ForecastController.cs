using Microsoft.AspNetCore.Mvc;
using WeatherForecast.Infrastructure.Database.Entities;
using WeatherForecast.Infrastructure.Database.Repositories;

namespace WeatherForecast.ForecastParserService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ForecastController(IRepository<Forecast> forecastRepository) : ControllerBase
{
    [HttpGet]
    public Task<IEnumerable<Forecast>> GetAllRecords(int skip, int offset)
        => Task.FromResult(forecastRepository.GetAll());
}
