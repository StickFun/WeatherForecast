using Microsoft.EntityFrameworkCore;
using WeatherForecast.Infrastructure.Database.Entities;

namespace WeatherForecast.Infrastructure.Database.Contexts;

internal class BaseContext<TEntity> : DbContext
    where TEntity : class, IBaseEntity
{
    public DbSet<TEntity> Entities { get;}
}
