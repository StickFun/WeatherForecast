namespace WeatherForecast.Application.Abstractions;

public interface IFileManager
{
    void ExtractTo(Stream archiveStream, string destinationDirectoryPath);

    void DeleteDirectory(string directoryPath);

    bool IsDirectoryExists(string directoryPath);

    string CreateDirectory(string directoryPath);

    List<string> GetExcelFilePaths(string directoryPath);
}
