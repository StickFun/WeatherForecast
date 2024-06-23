using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WeatherForecast.Infrastructure.Database.Contexts;
using WeatherForecast.Infrastructure.Database.Entities;

namespace WeatherForecast.Infrastructure.Database.Repositories;

internal class Repository<TEntity>(BaseContext<TEntity> context) : IRepository<TEntity>
    where TEntity : class, IBaseEntity
{
    private bool _isDisposed = false;

    private DbSet<TEntity> Entities
        => context.Entities;

    public ValueTask<EntityEntry<TEntity>> Create(TEntity record)
        => Entities.AddAsync(record);

    public Task CreateRange(List<TEntity> records)
        => Entities.AddRangeAsync(records);

    public async Task Delete(Guid id)
    {
        var record = await GetRecord(id);
        if (record != null)
            Entities.Remove(record);
    }

    public Task<List<TEntity>> GetAll()
        => Task.FromResult(Entities.ToList());

    public ValueTask<TEntity?> GetRecord(Guid id)
        => Entities.FindAsync(id);

    public Task<int> Save()
        => context.SaveChangesAsync();

    public Task<EntityEntry<TEntity>> Update(TEntity record)
        => Task.FromResult(Entities.Update(record));

    public void Dispose(bool disposing)
    {
        if (_isDisposed)
        {
            if (disposing)
            {
                context.Dispose();
            }
        }

        _isDisposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
