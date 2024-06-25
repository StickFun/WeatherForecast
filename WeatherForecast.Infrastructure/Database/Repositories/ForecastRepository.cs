using Microsoft.EntityFrameworkCore;
using WeatherForecast.Infrastructure.Database.Contexts;
using WeatherForecast.Infrastructure.Database.Entities;

namespace WeatherForecast.Infrastructure.Database.Repositories;
internal class ForecastRepository(ForecastContext context) : IRepository<Forecast>
{
    private DbSet<Forecast> Forecasts
        => context.Forecasts;

    public Forecast Create(Forecast record)
        => Forecasts.Add(record).Entity;

    public void CreateRange(List<Forecast> records)
        => Forecasts.AddRange(records);

    public void Delete(Guid id)
        => Forecasts.Remove(GetRecord(id));

    public IEnumerable<Forecast> GetAll()
        => Forecasts;

    public Forecast? GetRecord(Guid id)
        => Forecasts.Find(id);

    public int Save()
        => context.SaveChanges();

    public Forecast Update(Forecast record)
        => Forecasts.Update(record).Entity;
}
