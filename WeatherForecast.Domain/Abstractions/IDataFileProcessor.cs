namespace WeatherForecast.Domain.Abstractions;

public interface IDataFileProcessor
{
    Task Process(IDataFile dataFile);
}
