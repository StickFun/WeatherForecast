namespace WeatherForecast.Domain.Abstractions;

public interface IDataFile
{
    string GetFilePath();

    Guid GetGuid();
}
