using Microsoft.EntityFrameworkCore.ChangeTracking;
using WeatherForecast.Infrastructure.Database.Entities;

namespace WeatherForecast.Infrastructure.Database.Repositories;

internal interface IRepository<TEntity> : IDisposable
        where TEntity : class, IBaseEntity
{
    Task<List<TEntity>> GetAll();

    ValueTask<TEntity?> GetRecord(Guid id);

    ValueTask<EntityEntry<TEntity>> Create(TEntity record);

    Task CreateRange(List<TEntity> records);

    Task<EntityEntry<TEntity>> Update(TEntity record);

    Task Delete(Guid id);

    Task<int> Save();
}
