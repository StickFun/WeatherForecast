using Microsoft.EntityFrameworkCore;
using WeatherForecast.Infrastructure.Database.Entities;

namespace WeatherForecast.Infrastructure.Database.Contexts;

public class ArchiveContext(DbContextOptions<ArchiveContext> options) : DbContext(options)
{
    public DbSet<Archive> Archives { get; set; }
}
