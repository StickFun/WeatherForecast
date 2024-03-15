using Microsoft.EntityFrameworkCore;
using Personal.Project.DatabaseLibrary.Entities;

namespace Personal.Project.DatabaseLibrary.Contexts
{
    #region Class: ArchiveContext
    /// <summary>
    /// Контекст сущности архива погоды базы данных.
    /// </summary>
    public class ArchiveContext : DbContext
    {
        #region Properties: Public
        /// <summary>
        /// Записи сущности архивов.
        /// </summary>
        public DbSet<Archive> WeatherArchives { get; set; }
        #endregion

        #region Methods: Private
        /// <summary>
        /// Возвращает настройки контекста базы данных.
        /// </summary>
        /// <param name="connectionString">Строка подключения.</param>
        /// <returns>Настройки контекста базы данных.</returns>
        private static DbContextOptions<ArchiveContext> GetDbContextOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder<ArchiveContext>(), connectionString).Options;
        }
        #endregion

        #region Constructors: Public
        public ArchiveContext(string connectionString)
            : base(GetDbContextOptions(connectionString)) // Передавать строку напрямую
        {
            Database.EnsureCreated();
        }
        #endregion
    }
    #endregion
}
