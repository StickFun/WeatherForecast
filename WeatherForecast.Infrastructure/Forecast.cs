using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherForecast.Infrastructure;

[Table("Forecast")]
public class Forecast
{
    [Key]
    public Guid Id { get; set; }

    [Column("Datetime")]
    public DateTime Datetime { get; set; }

    [Column("AirTemperature")]
    public float AirTemperature { get; set; }

    [Column("RelativeAirHumidityPercent")]
    public float RelativeAirHumidityPercent { get; set; }

    [Column("DewPoint")]
    public float DewPoint { get; set; }

    [Column("AtmosphericPressure")]
    public float AtmosphericPressure { get; set; }

    [Column("WindDirection")]
    public int WindDirection { get; set; }

    [Column("WindSpeed")]
    public int WindSpeed { get; set; }

    [Column("Cloudiness")]
    public int Cloudiness { get; set; }

    [Column("LowerCloudEdge")]
    public int LowerCloudEdge { get; set; }

    [Column("HorizontalVisibility")]
    public int HorizontalVisibility { get; set; }

    [Column("WeatherEvents")]
    public string? WeatherEvents { get; set; }

    [ForeignKey("ArchiveId")]
    public Guid ArchiveId { get; set; }

    public override string ToString()
    {
        return $"{Datetime}" +
            $", {AirTemperature}" +
            $", {DewPoint}" +
            $", {AtmosphericPressure}" +
            $", {WindDirection}" +
            $", {WindSpeed}" +
            $", {Cloudiness}" +
            $", {LowerCloudEdge}" +
            $", {HorizontalVisibility}" +
            $", {WeatherEvents}";
    }
}
