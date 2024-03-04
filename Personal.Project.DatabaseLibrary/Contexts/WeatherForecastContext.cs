using Microsoft.EntityFrameworkCore;
using Personal.Project.DatabaseLibrary.Entities;
using Personal.Project.ValidationLibrary;

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
        public DbSet<WeatherForecast> WeatherForecasts { get; set; }
        #endregion
    }
    #endregion
}
