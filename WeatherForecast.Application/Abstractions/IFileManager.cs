namespace WeatherForecast.Application.Abstractions;

public interface IFileManager
{
    void ExtractTo(string zipFilePath, string destinationDirectoryPath);

    void DeleteDirectory(string directoryPath);

    bool IsDirectoryExists(string directoryPath);

    string CreateTempDirectory();

    List<string> GetExcelFilePaths(string directoryPath);
}
