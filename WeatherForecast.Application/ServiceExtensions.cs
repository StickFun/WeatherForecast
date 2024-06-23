using Microsoft.Extensions.DependencyInjection;
using WeatherForecast.Application.Common;
using WeatherForecast.Domain.Abstractions;

namespace WeatherForecast.Application;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
        => services.AddCommon();

    internal static IServiceCollection AddCommon(this IServiceCollection services)
        => services
        .AddTransient<IDataFileProvider, DataFileProvider>()
        .AddSingleton<IWeatherForecastContext, WeatherForecastContext>();
}
