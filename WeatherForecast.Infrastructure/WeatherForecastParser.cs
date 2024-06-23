using WeatherForecast.Application.Abstractions;

namespace WeatherForecast.Infrastructure;

internal class WeatherForecastParser(IExcelService excelService) : IWeatherForecastParser
{
    public void ExcelFileToDatabase(string excelFilePath)
    {
        var forecasts = excelService.GetForecastRecords(excelFilePath);
    }
}
