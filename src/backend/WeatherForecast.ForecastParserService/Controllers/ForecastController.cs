using Microsoft.AspNetCore.Mvc;
using WeatherForecast.Infrastructure.Database.Entities;
using WeatherForecast.Infrastructure.Database.Services;

namespace WeatherForecast.ForecastParserService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ForecastController(IForecastService forecastService) : ControllerBase
{
    [HttpGet]
    public Task<List<Forecast>> GetAllRecords(int skip, int take)
        => forecastService.GetForecasts(skip, take);
}
