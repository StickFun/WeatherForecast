using IronXL;
using Personal.Project.FileSystemLibrary.Enums;
using Personal.Project.ValidationLibrary;

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
        /// Фрагмент текста ошибки получения значения..
        /// </summary>
        private const string _exceptionValue = "Не удалось получить значение";
        #endregion

        #region Methods: Public

        #endregion

        #region Methods: Private
        private void ValidateWorkBooks()
        {
            foreach(var excelPath in _excelFilePaths)
            {
                var workBook = WorkBook.Load(excelPath);

            }
        }

        private void ProcessWorkSheet(WorkSheet workSheet)
        {
            var rowIndex = _rowIndexDataStart;

            while (!workSheet.GetRow(rowIndex).IsEmpty)
            {
                var row = workSheet.GetRow(_rowIndexDataStart);


                var weatherForecastRecord = TryParseWeatherForecastRecord(row);

                rowIndex++;
            }

        }

        private bool TryParseWeatherForecastRecord(RangeRow row)
        {
            var cellIndex = 0;

            foreach (var cell in row)
            {
                try
                {
                    switch ((WeatherForecastFieldSequence)cellIndex)
                    {
                        case WeatherForecastFieldSequence.Date:
                            if (!DateTime.TryParse(cell.Text, out DateTime date))
                            {
                                throw new ArgumentException($"{_exceptionValue} даты. [{cell.Address}]");
                            }
                            break;
                        case WeatherForecastFieldSequence.Time:
                            if (!DateTime.TryParse(cell.Text, out DateTime time))
                            {
                                throw new ArgumentException($"Не удалось получить значение времени. [{cell.Address}]");
                            }
                            break;
                        case WeatherForecastFieldSequence.AirTemperature:
                            if (!float.TryParse(cell.Text, out float airTemperature))
                            {
                                throw new ArgumentException($"Не удалось получить значение температуры воздуха. [{cell.Address}]");
                            }
                            break;
                        case WeatherForecastFieldSequence.RelativeAirHumidityPercent:
                            if (!float.TryParse(cell.Text, out float relativeAirHumidityPercent))
                            {
                                throw new ArgumentException($"Не удалось получить значение температуры воздуха. [{cell.Address}]");
                            }
                            break;
                        case WeatherForecastFieldSequence.DewPoint:
                            if (!float.TryParse(cell.Text, out float dewPoint))
                            {
                                throw new ArgumentException($"Не удалось получить значение точки росы. [{cell.Address}]");
                            }
                            break;
                        case WeatherForecastFieldSequence.AtmosphericPressure:
                            if (!float.TryParse(cell.Text, out float atmosphericPressure))
                            {
                                throw new ArgumentException($"Не удалось получить значение атмосферного давления. [{cell.Address}]");
                            }
                            break;
                        case WeatherForecastFieldSequence.WindDirection:
                            if (!int.TryParse(cell.Text, out int windDirection))
                            {
                                throw new ArgumentException($"Не удалось получить значение направления ветра. [{cell.Address}]");
                            }
                            break;
                        case WeatherForecastFieldSequence.WindSpeed:
                            if (!float.TryParse(cell.Text, out float windSpeed))
                            {
                                throw new ArgumentException($"Не удалось получить значение скорости ветра. [{cell.Address}]");
                            }
                            break;
                        case WeatherForecastFieldSequence.Cloudiness:
                            if (!int.TryParse(cell.Text, out int cloudiness))
                            {
                                throw new ArgumentException($"Не удалось получить значение облачности. [{cell.Address}]");
                            }
                            break;
                        case WeatherForecastFieldSequence.LowerCloudEdge:
                            if (!int.TryParse(cell.Text, out int horizontalVisibility))
                            {
                                throw new ArgumentException($"Не удалось получить значение горизонтальную видимость.");
                            }
                            break;
                        case WeatherForecastFieldSequence.HorizontalVisibility:
                            break;
                        case WeatherForecastFieldSequence.WeatherEvents:
                            break;

                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex);
                }


                cellIndex++;
            }
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
