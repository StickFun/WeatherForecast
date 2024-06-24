using WeatherForecast.ForecastParserService.Common;

namespace WeatherForecast.ForecastParserService.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddForecastParserServices(this IServiceCollection services)
        => services
        .AddCommon();


    internal static IServiceCollection AddCommon(this IServiceCollection services)
        => services
        .AddTransient<IUploadedFileService, UploadedFileService>();
}
