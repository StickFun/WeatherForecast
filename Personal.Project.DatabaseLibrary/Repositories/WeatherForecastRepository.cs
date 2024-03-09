using Personal.Project.DatabaseLibrary.Contexts;
using Personal.Project.DatabaseLibrary.Entities;
using Personal.Project.ValidationLibrary;

namespace Personal.Project.DatabaseLibrary.Repositories
{
    #region Class: WeatherForecastRepository
    /// <summary>
    /// Репозиторий сущности прогноза погоды.
    /// </summary>
    internal class WeatherForecastRepository : IRepository<WeatherForecast>
    {
        #region Fields: Private
        /// <summary>
        /// Контекст прогнозов погоды.
        /// </summary>
        private readonly WeatherForecastContext _weatherForecastContext;

        /// <summary>
        /// Отвечает за утилизацию объекта.
        /// </summary>
        private bool _isDisposed = false;
        #endregion

        #region Methods: Public
        public void Create(WeatherForecast record)
        {
            ObjectValidator<WeatherForecast>.CheckIsNull(record);
            _weatherForecastContext.WeatherForecasts.Add(record);
        }

        public void Delete(Guid id)
        {
            var record = _weatherForecastContext.WeatherForecasts.Find(id);

            if (record != null)
            {
                _weatherForecastContext.WeatherForecasts.Remove(record);
            }
        }

        public IEnumerable<WeatherForecast> GetAll()
        {
            return _weatherForecastContext.WeatherForecasts;
        }

        public WeatherForecast GetRecord(Guid id)
        {
            return _weatherForecastContext.WeatherForecasts.Find(id);
        }

        public void Save()
        {
            _weatherForecastContext.SaveChanges();
        }

        public void Update(WeatherForecast record)
        {
            ObjectValidator<WeatherForecast>.CheckIsNull(record);
        }

        /// <summary>
        /// Утилизирует контекст прогноза погоды.
        /// </summary>
        /// <param name="disposing">Отвечает за выполнение утилизация репозитория.</param>
        public void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                if (disposing)
                {
                    _weatherForecastContext.Dispose();
                }
            }

            _isDisposed = true;
        }

        /// <summary>
        /// Утилизирует объект.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #region Constructors: Public
        public WeatherForecastRepository(string connectionString)
        {
            StringValidator.CheckIsNullOrWhitespace(connectionString);
            _weatherForecastContext = new WeatherForecastContext(connectionString);
        }
        #endregion
    }
    #endregion
}
