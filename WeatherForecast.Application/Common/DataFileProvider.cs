using Microsoft.Extensions.Logging;
using WeatherForecast.Application.Abstractions;
using WeatherForecast.Domain.Abstractions;

namespace WeatherForecast.Application.Common;

internal class DataFileProvider(
    ILogger<DataFileProvider> logger,
    WeatherForecastContext context,
    IFileManager fileManager) : IDataFileProvider
{

    public Task<List<IDataFile>> Get()
    {
        var dataFilePaths = fileManager.GetExcelFilePaths(directoryPath);
        var dataFileList = new List<IDataFile>();

        foreach (var filePath in dataFilePaths)
        {

        }
    }
}
