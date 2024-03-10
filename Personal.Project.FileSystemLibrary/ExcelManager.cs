using IronXL;
using Microsoft.IdentityModel.Tokens;
using Personal.Project.DatabaseLibrary.Adapters;
using Personal.Project.DatabaseLibrary.Entities;
using Personal.Project.FileSystemLibrary.Enums;
using Personal.Project.ValidationLibrary;
using System.Diagnostics;

namespace Personal.Project.FileSystemLibrary
{
    #region Class: ExcelManager
    /// <summary>
    /// Взаимодействует с Excel файлами.
    /// </summary>
    public class ExcelManager
    {
        #region Fields: Private
        /// <summary>
        /// Список путей до Excel файлов.
        /// </summary>
        private List<string> _excelFilePaths;

        /// <summary>
        /// Индекс строки начала данных.
        /// </summary>
        private const int _rowIndexDataStart = 4;

        /// <summary>
        /// Индекс строки заголовков.
        /// </summary>
        private const int _rowHeaderIndex = 2;
        #endregion

        #region Methods: Public
        /// <summary>
        /// Добавляет прочитанные записи прогнозов погоды в базу данных.
        /// </summary>
        public void ParseRecordsToDatabase()
        {
            foreach (var excelPath in _excelFilePaths)
            {
                // todo: добавить проверку на занятость другим процессом.
                var workBook = WorkBook.Load(excelPath);

                foreach (var workSheet in workBook.WorkSheets)
                {
                    ProcessWorkSheet(workSheet);
                }
            }
        }
        #endregion

        #region Methods: Private
        /// <summary>
        /// Удаляет пути до невалидных таблиц.
        /// </summary>
        private void RemoveInvalidWorkBooks()
        {
            foreach (var excelPath in _excelFilePaths)
            {
                var workBook = WorkBook.Load(excelPath);
                var isValidWorkBook = true;

                foreach (var workSheet in workBook.WorkSheets)
                {
                    var headerRow = workSheet.GetRow(_rowHeaderIndex);

                    if (!IsValidHeaderRow(headerRow))
                    {
                        isValidWorkBook = false;
                        break;
                    }
                }

                if (!isValidWorkBook)
                {
                    _excelFilePaths.Remove(excelPath);
                }
            }
        }

        private bool IsValidHeaderRow(RangeRow row)
        {
            var cellIndex = 0;

            foreach (var cell in row)
            {
                cellIndex++;
            }
            return true;
        }

        private void ProcessWorkSheet(WorkSheet workSheet)
        {
            var rowIndex = _rowIndexDataStart;

            while (rowIndex < workSheet.RangeAddress.LastRow)
            {
                var row = workSheet.GetRow(rowIndex);

                try
                {
                    var parsedWeatherForecast = ParseWeatherForecastRecord(row);
                    Console.WriteLine(parsedWeatherForecast);
                    // todo: добавить запись прогноза погоды.
                }
                catch (ArgumentException ex)
                {
                    Trace.WriteLine(ex.Message);
                    // todo: добавить вывод ошибки.
                }

                rowIndex++;
            }
        }

        /// <summary>
        /// Возвращает полученный WeatherForecast из строки Excel.
        /// </summary>
        /// <param name="row">Строка Excel.</param>
        /// <returns>Объект WeatherForecast.</returns>
        /// <exception cref="ArgumentException">Не удалось получить значение ячейки.</exception>
        private static WeatherForecast ParseWeatherForecastRecord(RangeRow row)
        {
            var cellIndex = 0;
            var weatherForecast = new WeatherForecast();
            
            foreach (var cell in row)
            {
                switch ((WeatherForecastColumnSequence)cellIndex)
                {
                    case WeatherForecastColumnSequence.Date:
                        if (!DateTime.TryParse(cell.Text, out DateTime date))
                        {
                            throw new ArgumentException($"Не удалось получить значение даты. [{cell.Address}]");
                        }

                        weatherForecast.ForecastDatetime = date;
                        break;
                    case WeatherForecastColumnSequence.Time:
                        if (!TimeSpan.TryParse(cell.Text, out TimeSpan time))
                        {
                            throw new ArgumentException($"Не удалось получить значение времени. [{cell.Address}]");
                        }

                        weatherForecast.ForecastDatetime = weatherForecast.ForecastDatetime + time;
                        break;
                    case WeatherForecastColumnSequence.AirTemperature:
                        if (!float.TryParse(cell.Text, out float airTemperature))
                        {
                            throw new ArgumentException($"Не удалось получить значение температуры воздуха. [{cell.Address}]");
                        }

                        weatherForecast.AirTemperature = airTemperature;
                        break;
                    case WeatherForecastColumnSequence.RelativeAirHumidityPercent:
                        if (!float.TryParse(cell.Text, out float relativeAirHumidityPercent))
                        {
                            throw new ArgumentException($"Не удалось получить значение относительной влажности воздуха. [{cell.Address}]");
                        }

                        weatherForecast.RelativeAirHumidityPercent = relativeAirHumidityPercent;
                        break;
                    case WeatherForecastColumnSequence.DewPoint:
                        if (!float.TryParse(cell.Text, out float dewPoint))
                        {
                            throw new ArgumentException($"Не удалось получить значение точки росы. [{cell.Address}]");
                        }

                        weatherForecast.DewPoint = dewPoint;
                        break;
                    case WeatherForecastColumnSequence.AtmosphericPressure:
                        if (!float.TryParse(cell.Text, out float atmosphericPressure))
                        {
                            throw new ArgumentException($"Не удалось получить значение атмосферного давления. [{cell.Address}]");
                        }

                        weatherForecast.AtmosphericPressure = atmosphericPressure;
                        break;
                    case WeatherForecastColumnSequence.WindDirection:
                        if (string.IsNullOrWhiteSpace(cell.Text))
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
                    case WeatherForecastColumnSequence.HorizontalVisibility:
                        if (!string.IsNullOrWhiteSpace(cell.Text) && int.TryParse(cell.Text, out int horizontalVisibility))
                        {
                            weatherForecast.HorizontalVisibility = horizontalVisibility;
                        }

                        break;
                    case WeatherForecastColumnSequence.WeatherEvents:
                        weatherForecast.WeatherEvents = cell.Text;
                        break;
                }

                cellIndex++;
            }

            return weatherForecast;
        }
        #endregion

        #region Constructors: Public
        public ExcelManager(List<string> excelFilePaths)
        {
            ObjectValidator<List<string>>.CheckIsNull(excelFilePaths);

            if (excelFilePaths.Count == 0)
            {
                throw new ArgumentException(
                    "Список путей до Excel файлов не может быть пустым.",
                    nameof(excelFilePaths));
            }

            _excelFilePaths = excelFilePaths;
        }
        #endregion
    }
    #endregion
}
