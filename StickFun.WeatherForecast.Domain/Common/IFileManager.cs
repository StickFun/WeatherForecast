namespace StickFun.WeatherForecast.Domain.Common;

public interface IFileManager
{
    Task<List<IExcelFile>> GetExcelFiles(string directoryPath);

    Task<bool> IsDirectoryExists(string directoryPath);
}
