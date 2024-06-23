using WeatherForecast.Application.Abstractions;
using WeatherForecast.Domain.Abstractions;

namespace WeatherForecast.Application.Common;

internal class ExcelDataFileProcessor(IForecastParser parser) : IDataFileProcessor
{
    public Task Process(IDataFile dataFile)
        => parser.ExcelFileToDatabase(dataFile.GetFilePath(), dataFile.GetGuid());
}
