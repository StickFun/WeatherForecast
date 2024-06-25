namespace WeatherForecast.ForecastParserService.Common;

public interface IUploadedFileService
{
    Task ProcessZipFile(Stream zipStream, string fileName);
}