using Microsoft.Extensions.Logging;
using WeatherForecast.Domain.Abstractions;

namespace WeatherForecast.Domain;

public class FileDataProcessor(
    IDataFileProvider dataFileProvider,
    ILogger<FileDataProcessor> logger)
{
    public async Task ParseWeatherForecast()
    {
        foreach(var dataFile in await dataFileProvider.Get())
        {
            try
            {
                dataFile.Process();
            }
            catch(Exception ex)
            {
                logger.LogError("Не удалось обработать файл '{fileName}'. Ошибка: {exception}", Path.GetFileName(dataFile.GetFilePath()), ex);
            }
        }
    }
}
