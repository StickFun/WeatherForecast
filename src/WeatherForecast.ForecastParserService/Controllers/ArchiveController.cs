using Microsoft.AspNetCore.Mvc;
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
        file.ThrowIfEmpty();
        file.ThrowIfNotArchive();
        await uploadedFileService.ProcessZipFile(file.OpenReadStream(), file.FileName);
        return Ok();
    }
}
