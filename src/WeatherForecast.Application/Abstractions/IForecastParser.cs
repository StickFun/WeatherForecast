namespace WeatherForecast.Application.Abstractions;

public interface IForecastParser
{
    Task ExcelFileToDatabase(string excelFilePath, Guid archiveId);
}
