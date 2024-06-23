namespace WeatherForecast.Domain.Abstractions;

public interface IDataFileProvider
{
    Task<List<IDataFile>> Get();
}
