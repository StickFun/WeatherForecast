using Microsoft.EntityFrameworkCore.ChangeTracking;
using WeatherForecast.Infrastructure.Database.Entities;

namespace WeatherForecast.Infrastructure.Database.Repositories;

public interface IRepository<TEntity>
        where TEntity : class, IBaseEntity
{
    IEnumerable<TEntity> GetAll();

    TEntity GetRecord(Guid id);

    TEntity Create(TEntity record);

    void CreateRange(List<TEntity> records);

    TEntity Update(TEntity record);

    void Delete(Guid id);

    int Save();
}
