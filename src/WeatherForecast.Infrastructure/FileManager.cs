using System.IO.Compression;
using WeatherForecast.Application.Abstractions;

namespace WeatherForecast.Infrastructure;

internal class FileManager : IFileManager
{
    private const string _excelExtension = "*.xlsx";

    public string CreateDirectory(string directoryPath)
    {
        DeleteDirectory(directoryPath);
        Directory.CreateDirectory(directoryPath);

        return directoryPath;
    }

    public void DeleteDirectory(string directoryPath)
    {
        if (Directory.Exists(directoryPath))
            Directory.Delete(directoryPath, true);
    }

    public void ExtractTo(Stream archiveStream, string destinationDirectoryPath)
        => ZipFile.ExtractToDirectory(archiveStream, destinationDirectoryPath);

    public List<string> GetExcelFilePaths(string directoryPath)
        => Directory.EnumerateFiles(directoryPath, _excelExtension, SearchOption.AllDirectories).ToList();

    public bool IsDirectoryExists(string directoryPath)
        => Directory.Exists(directoryPath);
}
