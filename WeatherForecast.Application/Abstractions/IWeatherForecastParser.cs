namespace WeatherForecast.Application.Abstractions;

public interface IWeatherForecastParser
{
    void ExcelFileToDatabase(string excelFilePath);
}
