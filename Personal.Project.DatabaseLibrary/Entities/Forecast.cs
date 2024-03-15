using Personal.Project.DatabaseLibrary.Adapters;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Personal.Project.DatabaseLibrary.Entities
{
    #region Class: Forecast
    /// <summary>
    /// Прогноз погоды.
    /// </summary>
    [Table("Forecast")]
    public class Forecast
    {
        #region Properties: Public
        /// <summary>
        /// Идентификатор.
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Дата прогноза.
        /// </summary>
        [Column("ForecastDatetime")]
        public DateTime ForecastDatetime { get; set; }

        /// <summary>
        /// Температура воздуха.
        /// </summary>
        [Column("AirTemperature")]
        public float AirTemperature { get; set; }

        /// <summary>
        /// Относительная влажность воздуха в процентах.
        /// </summary>
        [Column("RelativeAirHumidityPercent")]
        public float RelativeAirHumidityPercent { get; set; }

        /// <summary>
        /// Точка росы.
        /// </summary>
        [Column("DewPoint")]
        public float DewPoint { get; set; }

        /// <summary>
        /// Атмосферное давление.
        /// </summary>
        [Column("AtmosphericPressure")]
        public float AtmosphericPressure { get; set; }

        /// <summary>
        /// Направление ветра.
        /// </summary>
        [Column("WindDirection")]
        public int WindDirection { get; set; }

        /// <summary>
        /// Скорость ветра.
        /// </summary>
        [Column("WindSpeed")]
        public int WindSpeed { get; set; }

        /// <summary>
        /// Облачность.
        /// </summary>
        [Column("Cloudiness")]
        public int Cloudiness { get; set; }

        /// <summary>
        /// Нижняя облачная граница.
        /// </summary>
        [Column("LowerCloudEdge")]
        public int LowerCloudEdge { get; set; }

        /// <summary>
        /// Горизонтальная видимость.
        /// </summary>
        [Column("HorizontalVisibility")]
        public int HorizontalVisibility { get; set; }

        /// <summary>
        /// Погодные явления.
        /// </summary>
        [Column("WeatherEvents")]
        public string? WeatherEvents { get; set; }

        /// <summary>
        /// Идентификатор архива.
        /// </summary>
        [ForeignKey("ArchiveId")]
        public Guid ArchiveId { get; set; }
        #endregion

        #region Methods: Public
        public override string ToString()
        {
            return $"{ForecastDatetime}" +
                $", {AirTemperature}" +
                $", {DewPoint}" +
                $", {AtmosphericPressure}" +
                $", {WindDirectionAdapter.ConvertToStringValue(WindDirection)}" +
                $", {WindSpeed}" +
                $", {Cloudiness}" +
                $", {LowerCloudEdge}" +
                $", {HorizontalVisibility}" +
                $", {WeatherEvents}";
        }
        #endregion
    }
    #endregion
}
