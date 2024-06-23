using Microsoft.EntityFrameworkCore;
using Personal.Project.DatabaseLibrary.Entities;

namespace Personal.Project.DatabaseLibrary.Contexts
{
    #region Class: ForecastContext
    /// <summary>
    /// Контекст прогнозов погоды.
    /// </summary>
    public class ForecastContext : DbContext
    {
        #region Properties: Public
        /// <summary>
        /// Записи сущности прогнозов погоды.
        /// </summary>
        public DbSet<Forecast> WeatherForecasts { get; set; } = null!;
        #endregion

        #region Methods: Private
        /// <summary>
        /// Возвращает настройки контекста базы данных.
        /// </summary>
        /// <param name="connectionString">Строка подключения.</param>
        /// <returns>Настройки контекста базы данных.</returns>
        private static DbContextOptions<ForecastContext> GetDbContextOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder<ForecastContext>(), connectionString).Options;
        }
        #endregion

        #region Constructors: Public
        public ForecastContext(string connectionString)
            : base(GetDbContextOptions(connectionString))
        {
            Database.EnsureCreated();
        }
        #endregion
    }
    #endregion
}
