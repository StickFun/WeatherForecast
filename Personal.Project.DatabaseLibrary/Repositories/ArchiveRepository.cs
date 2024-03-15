using Personal.Project.DatabaseLibrary.Contexts;
using Personal.Project.DatabaseLibrary.Entities;
using Personal.Project.ValidationLibrary;

namespace Personal.Project.DatabaseLibrary.Repositories
{
    #region Class: ArchiveRepository
    /// <summary>
    /// Репозиторий сущности архивов.
    /// </summary>
    public class ArchiveRepository : IRepository<Archive>
    {
        #region Fields: Private
        /// <summary>
        /// Контекст сущности архивов прогнозов погоды.
        /// </summary>
        private readonly ArchiveContext _context;

        /// <summary>
        /// Отвечает за утилизацию объекта.
        /// </summary>
        private bool _isDisposed = false;
        #endregion

        #region Methods: Public
        public void Create(Archive record)
        {
            ObjectValidator<Archive>.CheckIsNull(record);
            _context.WeatherArchives.Add(record);
        }

        public void Delete(Guid id)
        {
            var record = _context.WeatherArchives.Find(id);

            if (record != null)
            {
                _context.WeatherArchives.Remove(record);
            }
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

        public IEnumerable<Archive> GetAll()
        {
            return _context.WeatherArchives;
        }

        public Archive GetRecord(Guid id)
        {
            return _context.WeatherArchives.Find(id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Archive record)
        {
            ObjectValidator<Archive>.CheckIsNull(record);
            _context.WeatherArchives.Update(record);
        }
        #endregion

        #region Constructors: Public
        public ArchiveRepository()
        {
            _context = new ArchiveContext("Data Source=LAPTOP;Initial Catalog=WeatherForecast;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
        #endregion
    }
    #endregion
}
