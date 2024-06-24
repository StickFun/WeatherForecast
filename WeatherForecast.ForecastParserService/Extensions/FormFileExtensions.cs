using WeatherForecast.Domain.Exceptions;

namespace WeatherForecast.ForecastParserService.Extensions;

public static class FormFileExtensions
{
    private static string[] _archiveExtensions =
        [
            ".zip",
            ".7z",
            ".rar"
        ];

    public static void ThrowIfEmpty(this IFormFile file, string? message = null)
    {
        if (file is null || file.Length == 0)
            throw new DomainException(message ?? nameof(file));
    }

    public static void ThrowIfNotArchive(this IFormFile file, string? message = null)
    {
        if (!_archiveExtensions.Contains(Path.GetExtension(file.FileName)))
            throw new DomainException(message ?? nameof(file));
    }
}
