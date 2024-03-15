using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Personal.Project.DatabaseLibrary.Entities;
using Personal.Project.DatabaseLibrary.Repositories;
using Personal.Project.FileSystemLibrary;

namespace Personal.Project.WeatherForecastApplication.Controllers
{
    #region Class: ArchiveController
    [Route("api/[controller]")]
    [ApiController]
    public class ArchiveController : ControllerBase
    {
        #region Fields: Private
        /// <summary>
        /// Путь для хранения файлов.
        /// </summary>
        private const string _contentPath = "..\\Content";

        /// <summary>
        /// Репозиторий сущности архивов погоды.
        /// </summary>
        private readonly ArchiveRepository _archiveRepository;

        /// <summary>
        /// Репозиторий сущности прогнозов погоды.
        /// </summary>
        private readonly ForecastRepository _forecastRepository;
        #endregion

        #region Methods: Public
        /// <summary>
        /// Обрабатывает загрузку архива.
        /// </summary>
        /// <param name="zipFile">Архив.</param>
        /// <returns>Результат загрузки.</returns>
        [HttpPost]
        public async Task<IActionResult> Upload([FromForm] IFormFile zipFile)
        {
            if (zipFile == null || zipFile.Length == 0)
            {
                return BadRequest("Файл не может быть пустым.");
            }

            var tempPath = FileManager.CreateTempDirectory(_contentPath);

            
                FileManager.ExtractTo(zipFile.OpenReadStream(), tempPath);
                var archiveId = Guid.NewGuid();

                var archive = new Archive()
                {
                    Id = archiveId,
                    Name = zipFile.FileName,
                };

                var excelPaths = FileManager.FindAllExcelPaths(tempPath);

                if (excelPaths.Count() == 0)
                {
                    RemoveTempFolder(tempPath);
                    return BadRequest("Отсутствуют Excel таблицы.");
                }

                _archiveRepository.Create(archive);
                _archiveRepository.Save();

                var excelManager = new ExcelManager(excelPaths);
                excelManager.SetWeatherRepository(_forecastRepository);
                excelManager.ParseArchiveRecordsToDatabase(archiveId);
            
                RemoveTempFolder(tempPath);

                return Ok();
        }
        #endregion

        #region Methods: Private
        /// <summary>
        /// Удаляет содержимое временной папки.
        /// </summary>
        /// <param name="tempFolderPath">Путь до папки.</param>
        private void RemoveTempFolder(string tempFolderPath)
        {
            FileManager.RemoveFolder(tempFolderPath);
        }
        #endregion

        #region Constructors: Pubic
        public ArchiveController() 
            : base()
        {
            _archiveRepository = new ArchiveRepository();
            _forecastRepository = new ForecastRepository();
        }
        #endregion
    }
    #endregion
}
