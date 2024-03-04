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
        public int RelativeAirHumidityPercent { get; set; }

        /// <summary>
        /// Атмосферное давление.
        /// </summary>
        public float AtmosphericPressure { get; set; }

        /// <summary>
        /// Направление ветра.
        /// </summary>
        public int WindDirection { get; set; }

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
        public required string WeatherEvents { get; set; }

        /// <summary>
        /// Идентификатор архива.
        /// </summary>
        public Guid WeatherArchiveId { get; set; }
        #endregion
    }
    #endregion
}
