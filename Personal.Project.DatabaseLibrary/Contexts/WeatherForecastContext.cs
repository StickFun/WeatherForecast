using Microsoft.EntityFrameworkCore;
using Personal.Project.DatabaseLibrary.Entities;

namespace Personal.Project.DatabaseLibrary.Contexts
{
    #region Class: WeatherForecastContext
    /// <summary>
    /// Контекст прогнозов погоды.
    /// </summary>
    public class WeatherForecastContext : DbContext
    {
        #region Properties: Public
        /// <summary>
        /// Записи сущности прогнозов погоды.
        /// </summary>
        public DbSet<WeatherForecast> WeatherForecasts { get; set; } = null!;
        #endregion

        #region Methods: Private
        /// <summary>
        /// Возвращает настройки контекста базы данных.
        /// </summary>
        /// <param name="connectionString">Строка подключения.</param>
        /// <returns>Настройки контекста базы данных.</returns>
        private static DbContextOptions<WeatherForecastContext> GetDbContextOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder<WeatherForecastContext>(), connectionString).Options;
        }
        #endregion

        #region Constructors: Public
        public WeatherForecastContext(string connectionString)
            : base(GetDbContextOptions(connectionString))
        {
            Database.EnsureCreated();
        }
        #endregion
    }
    #endregion
}
