using Microsoft.Extensions.Logging;
using WeatherForecast.Application.Abstractions;
using WeatherForecast.Infrastructure.Database.Entities;
using WeatherForecast.Infrastructure.Database.Repositories;
using WeatherForecast.Infrastructure.Excel;

namespace WeatherForecast.Infrastructure;

internal class ForecastParser(
    ILogger<ForecastParser> logger,
    IExcelService excelService,
    IRepository<Forecast> forecastRepository) : IForecastParser
{
    public async Task ExcelFileToDatabase(string excelFilePath, Guid ArchiveId)
    {
        logger.LogInformation("Выполняется парсинг файла {fileName}.", Path.GetFileName(excelFilePath));

        var records = await excelService.GetForecastRecords(excelFilePath);

        foreach (var record in records)
        {
            record.ArchiveId = ArchiveId;
            forecastRepository.Create(record);
        }
    }
}
