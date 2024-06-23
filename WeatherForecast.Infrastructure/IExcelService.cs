namespace WeatherForecast.Infrastructure;

internal interface IExcelService
{
    public IEnumerable<Forecast> GetForecastRecords(string excelFilePath);
}
