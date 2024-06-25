using Microsoft.EntityFrameworkCore;
using WeatherForecast.Infrastructure.Database.Entities;

namespace WeatherForecast.Infrastructure.Database.Contexts;

public class ForecastContext(DbContextOptions<ForecastContext> options) : DbContext(options)
{
    public DbSet<Forecast> Forecasts { get; set; }
}
