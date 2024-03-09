using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Personal.Project.FileSystemLibrary;

namespace Personal.Project.WeatherForecastApplication.Controllers
{
    #region Class: ZipController
    [Route("api/[controller]")]
    [ApiController]
    public class ZipController : ControllerBase
    {
        #region Fields: Private
        private const string _contentPath = "..\\Content";
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
            FileManager.UnzipTo(zipFile.OpenReadStream(), tempPath);

            return Ok();
        }
        #endregion
    }
    #endregion
}
