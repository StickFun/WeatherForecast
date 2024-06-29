using Microsoft.AspNetCore.Mvc;
using WeatherForecast.Domain.Exceptions;
using WeatherForecast.ForecastParserService.Common;
using WeatherForecast.ForecastParserService.Extensions;

namespace WeatherForecast.ForecastParserService.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ArchiveController(IUploadedFileService uploadedFileService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        try
        {
            file.ThrowIfEmpty("Файл не должен быть пустым.");
            file.ThrowIfNotArchive("Файл должен быть архивом.");
            await uploadedFileService.ProcessZipFile(file.OpenReadStream(), file.FileName);
        }
        catch(Exception ex)
        {
            if (ex is DomainException)
            {
                return BadRequest(ex.Message);
            }
            else
            {
                return BadRequest("Упс! произошла неизвестная ошибка. Подробности в логах.");
            }
        }
        
        return Ok();
    }
}
