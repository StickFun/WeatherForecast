using Microsoft.Extensions.Logging;
using WeatherForecast.Application.Abstractions;
using WeatherForecast.Application.Models;
using WeatherForecast.Domain.Abstractions;
using WeatherForecast.Domain.Exceptions;

namespace WeatherForecast.Application.Common;

internal class DataFileProvider(
    ILogger<DataFileProvider> logger,
    WeatherForecastContext context,
    IFileManager fileManager) : IDataFileProvider
{

    public Task<List<IDataFile>> Get()
    {
        logger.LogInformation("Выполняется получение списка файлов с данными.");

        var currentDataFileDirectoryPath = context.CurrentDataFileDirectoryPath;
        var dataFilePaths = fileManager.GetExcelFilePaths(currentDataFileDirectoryPath);

        if (!Guid.TryParse(Path.GetDirectoryName(currentDataFileDirectoryPath), out var archiveGuid))
            throw new DomainException($"Не получилось считать GUID из названия папки '{currentDataFileDirectoryPath}'.");

        var dataFileList = new List<IDataFile>();

        foreach (var filePath in dataFilePaths)
        {
            dataFileList.Add(new ExcelDataFile
            {
                ArchiveId = archiveGuid,
                FilePath = filePath
            });

            logger.LogTrace("Добавлен файл '{fileName}' для архива с GUID'{guid}'.", archiveGuid, Path.GetFileName(filePath));
        }

        logger.LogInformation("Список файлов с данными был успешно получен.");

        return Task.FromResult(dataFileList);
    }
}
