using Microsoft.EntityFrameworkCore;
using WeatherForecast.Infrastructure.Database.Contexts;
using WeatherForecast.Infrastructure.Database.Entities;

namespace WeatherForecast.Infrastructure.Database.Repositories;

internal class ArchiveRepository(ArchiveContext context) : IRepository<Archive>
{
    private DbSet<Archive> Archives
        => context.Archives;

    public Archive Create(Archive record)
        => Archives.Add(record).Entity;

    public void CreateRange(List<Archive> records)
        => Archives.AddRange(records);

    public void Delete(Guid id)
        => Archives.Remove(GetRecord(id));

    public IEnumerable<Archive> GetAll()
        => Archives;

    public Archive? GetRecord(Guid id)
        => Archives.Find(id);

    public int Save()
        => context.SaveChanges();

    public Archive Update(Archive record)
        => Archives.Update(record).Entity;
}
