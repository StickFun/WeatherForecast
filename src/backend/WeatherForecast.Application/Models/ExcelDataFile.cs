using WeatherForecast.Domain.Abstractions;

namespace WeatherForecast.Application.Models;

internal class ExcelDataFile : IDataFile
{
    public string FilePath { get; set; }

    public Guid ArchiveId { get; set; }

    public string GetFilePath()
        => FilePath;    

    public Guid GetGuid()
        => ArchiveId;
}
