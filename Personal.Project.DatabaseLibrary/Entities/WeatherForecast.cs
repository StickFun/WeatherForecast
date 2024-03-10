using Personal.Project.DatabaseLibrary.Adapters;

namespace Personal.Project.DatabaseLibrary.Entities
{
    #region Class: WeatherForecast
    /// <summary>
    /// Прогноз погоды.
    /// </summary>
    public class WeatherForecast
    {
        #region Properties: Public
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Дата прогноза.
        /// </summary>
        public DateTime ForecastDatetime { get; set; }

        /// <summary>
        /// Температура воздуха.
        /// </summary>
        public float AirTemperature { get; set; }

        /// <summary>
        /// Относительная влажность воздуха в процентах.
        /// </summary>
        public float RelativeAirHumidityPercent { get; set; }

        /// <summary>
        /// Точка росы.
        /// </summary>
        public float DewPoint { get; set; }

        /// <summary>
        /// Атмосферное давление.
        /// </summary>
        public float AtmosphericPressure { get; set; }

        /// <summary>
        /// Направление ветра.
        /// </summary>
        public int WindDirection { get; set; }

        /// <summary>
        /// Скорость ветра.
        /// </summary>
        public int WindSpeed { get; set; }

        /// <summary>
        /// Облачность.
        /// </summary>
        public int Cloudiness { get; set; }

        /// <summary>
        /// Нижняя облачная граница.
        /// </summary>
        public int LowerCloudEdge { get; set; }

        /// <summary>
        /// Горизонтальная видимость.
        /// </summary>
        public int HorizontalVisibility { get; set; }

        /// <summary>
        /// Погодные явления.
        /// </summary>
        public string WeatherEvents { get; set; }

        /// <summary>
        /// Идентификатор архива.
        /// </summary>
        public Guid WeatherArchiveId { get; set; }
        #endregion

        #region Methods: Public
        public override string ToString()
        {
            return $"{ForecastDatetime} | {WindDirectionAdapter.ConvertToStringValue(WindDirection)} | {AirTemperature}";
        }
        #endregion
    }
    #endregion
}
