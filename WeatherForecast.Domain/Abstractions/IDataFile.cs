namespace WeatherForecast.Domain.Abstractions;

public interface IDataFile
{
    string GetFilePath();

    public Task Process();
}
