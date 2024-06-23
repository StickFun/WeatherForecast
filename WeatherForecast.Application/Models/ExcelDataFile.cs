using WeatherForecast.Application.Abstractions;
using WeatherForecast.Domain.Abstractions;

namespace WeatherForecast.Application.Models;

internal class ExcelDataFile(IWeatherForecastParser parser) : IDataFile
{
    public string FilePath { get; set; }

    public string GetFilePath()
        => FilePath;

    public Task Process()
    {
        parser.ExcelFileToDatabase(FilePath);
    }
}
