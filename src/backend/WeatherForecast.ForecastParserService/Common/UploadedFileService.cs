using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore.Migrations;
using WeatherForecast.Application.Abstractions;
using WeatherForecast.Application.Common;
using WeatherForecast.Domain;
using WeatherForecast.Infrastructure.Database.Entities;
using WeatherForecast.Infrastructure.Database.Repositories;

namespace WeatherForecast.ForecastParserService.Common;

public class UploadedFileService(
    ILogger<UploadedFileService> logger,
    IFileManager fileManager,
    DataFileParser dataFileParser,
    WeatherForecastContext context,
    IRepository<Archive> archiveRepository) : IUploadedFileService
{
    private const string _tempDirectoryName = "Temp";

    public async Task ProcessZipFile(Stream fileStream, string fileName)
    {
        logger.LogInformation("Выполняется обработка архива.");

        var directoryGuid = Guid.NewGuid();
        var currentDirectory = Directory.GetCurrentDirectory();
        var tempDirectory = Path.Combine(currentDirectory, _tempDirectoryName, directoryGuid.ToString());

        fileManager.CreateDirectory(tempDirectory);
        fileManager.ExtractTo(fileStream, tempDirectory);
        
        context.CurrentDataFileDirectoryPath = tempDirectory;
        var archive = new Archive
        {
            Id = directoryGuid,
            Name = Path.GetFileNameWithoutExtension(fileName)
        };

        archiveRepository.Create(archive);
        archiveRepository.Save();

        await dataFileParser.ParseWeatherForecast();

        fileManager.DeleteDirectory(tempDirectory);
    }
}
