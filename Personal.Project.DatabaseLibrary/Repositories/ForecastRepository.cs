using Personal.Project.DatabaseLibrary.Contexts;
using Personal.Project.DatabaseLibrary.Entities;
using Personal.Project.ValidationLibrary;

namespace Personal.Project.DatabaseLibrary.Repositories
{
    #region Class: ForecastRepository
    /// <summary>
    /// Репозиторий сущности прогноза погоды.
    /// </summary>
    public class ForecastRepository : IRepository<Forecast>
    {
        #region Fields: Private
        /// <summary>
        /// Контекст прогнозов погоды.
        /// </summary>
        private readonly ForecastContext _context;

        /// <summary>
        /// Отвечает за утилизацию объекта.
        /// </summary>
        private bool _isDisposed = false;
        #endregion

        #region Methods: Public
        public void Create(Forecast record)
        {
            ObjectValidator<Forecast>.CheckIsNull(record);
            _context.WeatherForecasts.Add(record);
        }

        public void Delete(Guid id)
        {
            var record = _context.WeatherForecasts.Find(id);

            if (record != null)
            {
                _context.WeatherForecasts.Remove(record);
            }
        }

        public IEnumerable<Forecast> GetAll()
        {
            return _context.WeatherForecasts;
        }

        public Forecast GetRecord(Guid id)
        {
            return _context.WeatherForecasts.Find(id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Forecast record)
        {
            ObjectValidator<Forecast>.CheckIsNull(record);
            _context.WeatherForecasts.Update(record);
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
                    _context.Dispose();
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
        public ForecastRepository()
        {
            _context = new ForecastContext("Data Source=LAPTOP;Initial Catalog=WeatherForecast;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
        #endregion
    }
    #endregion
}
