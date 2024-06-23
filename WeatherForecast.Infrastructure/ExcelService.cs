using IronXL;
using Microsoft.Extensions.Logging;

namespace WeatherForecast.Infrastructure;

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

    public IEnumerable<Forecast> GetForecastRecords(string excelFilePath)
    {
        var workBook = WorkBook.Load(excelFilePath);
        var forecasts = new IEnumerable<Forecast>();

        foreach (var sheet in workBook.WorkSheets)
        {
            GetRecordsFromWorkSheet(sheet);
        }
    }

    private void GetRecordsFromWorkSheet(WorkSheet sheet)
    {
        var rowIndex = _rowIndexDataStart;

        while (rowIndex < sheet.RangeAddress.LastRow)
        {
            var row = sheet.GetRow(rowIndex);

            var parsedWeatherForecast = ParseForecastRecord(row);

            rowIndex++;
        }
    }

    private object ParseForecastRecord(RangeRow row)
    {
        var cellIndex = 0;
        var weatherForecast = new Forecast();

        foreach (var cell in row)
        {
            switch ((WeatherForecastColumnSequence)cellIndex)
            {
                case WeatherForecastColumnSequence.Date:
                    if (!string.IsNullOrWhiteSpace(cell.Text) && DateTime.TryParse(cell.Text, out DateTime date))
                    {
                        weatherForecast.ForecastDatetime = date;
                    }
                    break;
                case WeatherForecastColumnSequence.Time:
                    if (!string.IsNullOrWhiteSpace(cell.Text) && TimeSpan.TryParse(cell.Text, out TimeSpan time))
                    {
                        weatherForecast.ForecastDatetime = weatherForecast.ForecastDatetime + time;
                    }
                    break;
                case WeatherForecastColumnSequence.AirTemperature:
                    if (!string.IsNullOrWhiteSpace(cell.Text) && float.TryParse(cell.Text, out float airTemperature))
                    {
                        weatherForecast.AirTemperature = airTemperature;
                    }
                    break;
                case WeatherForecastColumnSequence.RelativeAirHumidityPercent:
                    if (!string.IsNullOrWhiteSpace(cell.Text) && float.TryParse(cell.Text, out float relativeAirHumidityPercent))
                    {
                        weatherForecast.RelativeAirHumidityPercent = relativeAirHumidityPercent;
                    }
                    break;
                case WeatherForecastColumnSequence.DewPoint:
                    if (!string.IsNullOrWhiteSpace(cell.Text) && float.TryParse(cell.Text, out float dewPoint))
                    {
                        weatherForecast.DewPoint = dewPoint;
                    }
                    break;
                case WeatherForecastColumnSequence.AtmosphericPressure:
                    if (!string.IsNullOrWhiteSpace(cell.Text) && float.TryParse(cell.Text, out float atmosphericPressure))
                    {
                        weatherForecast.AtmosphericPressure = atmosphericPressure;
                    }
                    break;
                case WeatherForecastColumnSequence.WindDirection:
                    if (!string.IsNullOrWhiteSpace(cell.Text))
                    {
                        weatherForecast.WindDirection = WindDirectionAdapter.ConvertToIntValue(cell.Text);
                    }
                    break;
                case WeatherForecastColumnSequence.WindSpeed:
                    if (!string.IsNullOrWhiteSpace(cell.Text) && int.TryParse(cell.Text, out int windSpeed))
                    {
                        weatherForecast.WindSpeed = windSpeed;
                    }

                    break;
                case WeatherForecastColumnSequence.Cloudiness:
                    if (!string.IsNullOrWhiteSpace(cell.Text) && int.TryParse(cell.Text, out int cloudiness))
                    {
                        weatherForecast.Cloudiness = cloudiness;
                    }

                    break;
                case WeatherForecastColumnSequence.LowerCloudEdge:
                    if (!string.IsNullOrWhiteSpace(cell.Text) && int.TryParse(cell.Text, out int lowerCloudEdge))
                    {
                        weatherForecast.LowerCloudEdge = lowerCloudEdge;
                    }

                    break;
                case ExcelForecastColumnSequence.HorizontalVisibility:
                    if (!string.IsNullOrWhiteSpace(cell.Text) && int.TryParse(cell.Text, out int horizontalVisibility))
                    {
                        weatherForecast.HorizontalVisibility = horizontalVisibility;
                    }

                    break;
                case ExcelForecastColumnSequence.WeatherEvents:
                    weatherForecast.WeatherEvents = cell.Text;
                    break;
            }

            cellIndex++;
        }

        return weatherForecast;
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
