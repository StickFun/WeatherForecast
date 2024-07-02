using IronXL;
using Microsoft.Extensions.Logging;
using WeatherForecast.Infrastructure.Database.Entities;

namespace WeatherForecast.Infrastructure.Excel;

internal class ExcelService(ILogger<ExcelService> logger) : IExcelService
{
    private const int _rowIndexDataStart = 4;
    private const int _rowHeaderIndex = 2;

    private readonly Dictionary<ExcelForecastColumnSequence, string> _columnHeaderNameDictionary =
            new Dictionary<ExcelForecastColumnSequence, string>()
            {
                { ExcelForecastColumnSequence.Date, "Дата" },
                { ExcelForecastColumnSequence.Time, "Время" },
                { ExcelForecastColumnSequence.AirTemperature, "Т" },
                { ExcelForecastColumnSequence.RelativeAirHumidityPercent, "Отн. влажность" },
                { ExcelForecastColumnSequence.DewPoint, "Td" },
                { ExcelForecastColumnSequence.AtmosphericPressure, "Атм. давление," },
                { ExcelForecastColumnSequence.WindDirection, "Направление" },
                { ExcelForecastColumnSequence.WindSpeed, "Скорость" },
                { ExcelForecastColumnSequence.Cloudiness, "Облачность," },
                { ExcelForecastColumnSequence.LowerCloudEdge, "h" },
                { ExcelForecastColumnSequence.HorizontalVisibility, "VV" },
                { ExcelForecastColumnSequence.WeatherEvents, "Погодные явления" },
            };

    public Task<List<Forecast>> GetForecastRecords(string excelFilePath)
    {
        logger.LogInformation("Выполняется получение списка записей из файла {fileName}.", Path.GetFileName(excelFilePath));
        var workBook = WorkBook.Load(excelFilePath);
        var forecasts = new List<Forecast>();

        foreach (var sheet in workBook.WorkSheets)
        {
            if (IsValidHeaderRow(sheet.GetRow(_rowHeaderIndex)))
            {
                forecasts.AddRange(GetRecordsFromWorkSheet(sheet));
            }
            else
            {
                logger.LogError("Страница '{sheetName}' в файле '{fileName}' содержит некорректный заголовок.", sheet.Name, Path.GetFileName(workBook.FilePath));
            }
        }

        return Task.FromResult(forecasts);
    }

    private List<Forecast> GetRecordsFromWorkSheet(WorkSheet sheet)
    {
        var rowIndex = _rowIndexDataStart;
        var forecasts = new List<Forecast>();

        while (rowIndex < sheet.RangeAddress.LastRow)
        {
            var row = sheet.GetRow(rowIndex);
            rowIndex++;

            forecasts.Add(ParseForecastRecord(row));
        }

        return forecasts;
    }

    private Forecast ParseForecastRecord(RangeRow row)
    {
        var cellIndex = 0;
        var forecast = new Forecast();

        foreach (var cell in row)
        {
            switch ((ExcelForecastColumnSequence)cellIndex)
            {
                case ExcelForecastColumnSequence.Date:
                    if (!string.IsNullOrWhiteSpace(cell.Text) && DateTime.TryParse(cell.Text, out DateTime date))
                    {
                        forecast.Datetime = date;
                    }
                    break;
                case ExcelForecastColumnSequence.Time:
                    if (!string.IsNullOrWhiteSpace(cell.Text) && TimeSpan.TryParse(cell.Text, out TimeSpan time))
                    {
                        forecast.Datetime = forecast.Datetime + time;
                    }
                    break;
                case ExcelForecastColumnSequence.AirTemperature:
                    if (!string.IsNullOrWhiteSpace(cell.Text) && double.TryParse(cell.Text, out var airTemperature))
                    {
                        forecast.AirTemperature = airTemperature;
                    }
                    break;
                case ExcelForecastColumnSequence.RelativeAirHumidityPercent:
                    if (!string.IsNullOrWhiteSpace(cell.Text) && int.TryParse(cell.Text, out var relativeAirHumidityPercent))
                    {
                        forecast.RelativeAirHumidityPercent = relativeAirHumidityPercent;
                    }
                    break;
                case ExcelForecastColumnSequence.DewPoint:
                    if (!string.IsNullOrWhiteSpace(cell.Text) && double.TryParse(cell.Text, out var dewPoint))
                    {
                        forecast.DewPoint = dewPoint;
                    }
                    break;
                case ExcelForecastColumnSequence.AtmosphericPressure:
                    if (!string.IsNullOrWhiteSpace(cell.Text) && int.TryParse(cell.Text, out var atmosphericPressure))
                    {
                        forecast.AtmosphericPressure = atmosphericPressure;
                    }
                    break;
                case ExcelForecastColumnSequence.WindDirection:
                    if (!string.IsNullOrWhiteSpace(cell.Text))
                    {
                        forecast.WindDirection = WindDirectionAdapter.ConvertToIntValue(cell.Text);
                    }
                    break;
                case ExcelForecastColumnSequence.WindSpeed:
                    if (!string.IsNullOrWhiteSpace(cell.Text) && int.TryParse(cell.Text, out int windSpeed))
                    {
                        forecast.WindSpeed = windSpeed;
                    }
                    break;
                case ExcelForecastColumnSequence.Cloudiness:
                    if (!string.IsNullOrWhiteSpace(cell.Text) && int.TryParse(cell.Text, out int cloudiness))
                    {
                        forecast.Cloudiness = cloudiness;
                    }
                    break;
                case ExcelForecastColumnSequence.LowerCloudEdge:
                    if (!string.IsNullOrWhiteSpace(cell.Text) && int.TryParse(cell.Text, out int lowerCloudEdge))
                    {
                        forecast.LowerCloudEdge = lowerCloudEdge;
                    }

                    break;
                case ExcelForecastColumnSequence.HorizontalVisibility:
                    if (!string.IsNullOrWhiteSpace(cell.Text) && int.TryParse(cell.Text, out int horizontalVisibility))
                    {
                        forecast.HorizontalVisibility = horizontalVisibility;
                    }

                    break;
                case ExcelForecastColumnSequence.WeatherEvents:
                    forecast.WeatherEvents = cell.Text;
                    break;
            }

            cellIndex++;
        }

        return forecast;
    }

    private bool IsValidHeaderRow(RangeRow row)
    {
        var cellIndex = 0;
        var isValidHeader = true;

        foreach (var cell in row)
        {
            if (cell.Text != _columnHeaderNameDictionary.GetValueOrDefault((ExcelForecastColumnSequence)cellIndex))
            {
                isValidHeader = false;
                break;
            }

            cellIndex++;
        }

        return isValidHeader;
    }
}
