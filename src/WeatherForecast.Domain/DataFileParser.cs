using Microsoft.Extensions.Logging;
using WeatherForecast.Domain.Abstractions;

namespace WeatherForecast.Domain;

public class DataFileParser(
    IDataFileProvider dataFileProvider,
    IDataFileProcessor dataFileProcessor,
    ILogger<DataFileParser> logger)
{
    public async Task ParseWeatherForecast()
    {
        logger.LogInformation("Выполняется парсинг прогнозов погоды из файлов.");

        foreach(var dataFile in await dataFileProvider.Get())
        {
            try
            {
                await dataFileProcessor.Process(dataFile);
            }
            catch(Exception ex)
            {
                logger.LogError("Не удалось обработать файл '{fileName}'. Ошибка: {exception}", Path.GetFileName(dataFile.GetFilePath()), ex);
            }
        }
    }
}
