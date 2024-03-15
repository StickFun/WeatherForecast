using IronXL;
using Personal.Project.DatabaseLibrary.Adapters;
using Personal.Project.DatabaseLibrary.Entities;
using Personal.Project.DatabaseLibrary.Repositories;
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

        /// <summary>
        /// Словарь последовательности колонки прогноза погоды и названия заголовка.
        /// </summary>
        private readonly Dictionary<WeatherForecastColumnSequence, string> _columnHeaderNameDictionary =
            new Dictionary<WeatherForecastColumnSequence, string>()
            {
                { WeatherForecastColumnSequence.Date, "Дата" },
                { WeatherForecastColumnSequence.Time, "Время" },
                { WeatherForecastColumnSequence.AirTemperature, "Т" },
                { WeatherForecastColumnSequence.RelativeAirHumidityPercent, "Отн. влажность" },
                { WeatherForecastColumnSequence.DewPoint, "Td" },
                { WeatherForecastColumnSequence.AtmosphericPressure, "Атм. давление," },
                { WeatherForecastColumnSequence.WindDirection, "Направление" },
                { WeatherForecastColumnSequence.WindSpeed, "Скорость" },
                { WeatherForecastColumnSequence.Cloudiness, "Облачность," },
                { WeatherForecastColumnSequence.LowerCloudEdge, "h" },
                { WeatherForecastColumnSequence.HorizontalVisibility, "VV" },
                { WeatherForecastColumnSequence.WeatherEvents, "Погодные явления" },
            };

        /// <summary>
        /// Репозиторий прогнозов погоды.
        /// </summary>
        private ForecastRepository _weatherForecastRepository;

        /// <summary>
        /// Идентификатор архива прогнозов.
        /// </summary>
        private Guid _archiveId;
        #endregion

        #region Methods: Public
        /// <summary>
        /// Устанавливает репозиторий прогнозов погоды, куда будут добавляться записи.
        /// </summary>
        /// <param name="weatherForecastRepository">Репозиторий прогнозов погоды.</param>
        public void SetWeatherRepository(ForecastRepository weatherForecastRepository)
        {
            ObjectValidator<ForecastRepository>.CheckIsNull(weatherForecastRepository);
            _weatherForecastRepository = weatherForecastRepository;
        }

        /// <summary>
        /// Добавляет прочитанные записи прогнозов погоды в базу данных.
        /// </summary>
        public void ParseArchiveRecordsToDatabase(Guid archiveId)
        {
            if (archiveId == Guid.Empty)
            {
                throw new ArgumentException("Идентификатор архива не может быть пустым.", nameof(archiveId));
            }

            _archiveId = archiveId;

            foreach (var excelPath in _excelFilePaths)
            {
                // todo: добавить проверку на занятость другим процессом.
                var workBook = WorkBook.Load(excelPath);

                foreach (var workSheet in workBook.WorkSheets)
                {
                    AddWorkSheetToDatabase(workSheet);
                }
            }

            _weatherForecastRepository.Save();
        }
        #endregion

        #region Methods: Private
        /// <summary>
        /// Удаляет пути до некорректных таблиц.
        /// </summary>
        private void RemoveInvalidWorkBooks()
        {
            var invalidWorkBooksPath = new List<string>();

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
                    invalidWorkBooksPath.Add(excelPath);
                }
            }

            foreach (var workBookPath in invalidWorkBooksPath)
            {
                _excelFilePaths.Remove(workBookPath);
            }
        }

        /// <summary>
        /// Проверяет строку названий заголовков прогноза погоды.
        /// </summary>
        /// <param name="row">Строка названий заголовков.</param>
        /// <returns>True, если строка содержит корректные названия. Иначе False.</returns>
        private bool IsValidHeaderRow(RangeRow row)
        {
            var cellIndex = 0;
            var isValidHeader = true;

            foreach (var cell in row)
            {
                if (cell.Text != _columnHeaderNameDictionary.GetValueOrDefault((WeatherForecastColumnSequence)cellIndex))
                {
                    isValidHeader = false; 
                    break;
                }

                cellIndex++;
            }

            return isValidHeader;
        }

        /// <summary>
        /// Обрабатывает страницу из Excel таблицы и добавляет записи в базу данных.
        /// </summary>
        /// <param name="workSheet">Страница Excel.</param>
        private void AddWorkSheetToDatabase(WorkSheet workSheet)
        {
            var rowIndex = _rowIndexDataStart;

            while (rowIndex < workSheet.RangeAddress.LastRow)
            {
                var row = workSheet.GetRow(rowIndex);

                try
                {
                    var parsedWeatherForecast = ParseWeatherForecastRecord(row);
                    parsedWeatherForecast.ArchiveId = _archiveId;
                    Console.WriteLine(parsedWeatherForecast);
                    _weatherForecastRepository.Create(parsedWeatherForecast);
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
        private static Forecast ParseWeatherForecastRecord(RangeRow row)
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
            RemoveInvalidWorkBooks();
        }
        #endregion
    }
    #endregion
}
