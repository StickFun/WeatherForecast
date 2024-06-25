using Microsoft.Extensions.DependencyInjection;

namespace WeatherForecast.Domain.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
        => services.AddTransient<DataFileParser>();
}
