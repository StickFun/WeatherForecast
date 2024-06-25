using WeatherForecast.Infrastructure.Database.Entities;

namespace WeatherForecast.Infrastructure.Excel;

internal interface IExcelService
{
    public Task<List<Forecast>> GetForecastRecords(string excelFilePath);
}
