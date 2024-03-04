using Personal.Project.DatabaseLibrary.Contexts;
using Personal.Project.DatabaseLibrary.Entities;

namespace Personal.Project.DatabaseLibrary.Repositories
{
    #region Class: WeatherForecastRepository
    /// <summary>
    /// Репозиторий прогноза погоды.
    /// </summary>
    internal class WeatherForecastRepository : IRepository<WeatherForecast>
    {
        #region Fields: Private
        /// <summary>
        /// Контекст прогнозов погоды.
        /// </summary>
        private readonly WeatherForecastContext _context;
        #endregion


        #region Methods: Public
        public void Create(WeatherForecast record)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WeatherForecast> GetAll()
        {
            throw new NotImplementedException();
        }

        public WeatherForecast GetRecord(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(WeatherForecast record)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Constructors: Public
        public WeatherForecastRepository()
        {
            _context = new WeatherForecastContext();
        }
        #endregion
    }
    #endregion
}
