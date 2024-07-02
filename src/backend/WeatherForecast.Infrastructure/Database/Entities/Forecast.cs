using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherForecast.Infrastructure.Database.Entities;

[Table("Forecast")]
public class Forecast : IBaseEntity
{
    [Key]
    public Guid Id { get; set; }

    [Column("Datetime")]
    public DateTime Datetime { get; set; }

    [Column("AirTemperature")]
    public double AirTemperature { get; set; }

    [Column("RelativeAirHumidityPercent")]
    public int RelativeAirHumidityPercent { get; set; }

    [Column("DewPoint")]
    public double DewPoint { get; set; }

    [Column("AtmosphericPressure")]
    public int AtmosphericPressure { get; set; }

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
