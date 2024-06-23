using Microsoft.Extensions.DependencyInjection;
using WeatherForecast.Application.Abstractions;
using WeatherForecast.Infrastructure.Database.Contexts;
using WeatherForecast.Infrastructure.Database.Entities;
using WeatherForecast.Infrastructure.Database.Repositories;
using WeatherForecast.Infrastructure.Excel;

namespace WeatherForecast.Infrastructure;

public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        => services
        .AddDatabase()
        .AddExcel()
        .AddTransient<IFileManager, FileManager>()
        .AddTransient<IForecastParser, ForecastParser>();

    internal static IServiceCollection AddDatabase(this IServiceCollection services)
        => services
        .AddTransient<BaseContext<Forecast>>()
        .AddTransient<BaseContext<Archive>>()
        .AddTransient<IRepository<Forecast>, Repository<Forecast>>()
        .AddTransient<IRepository<Archive>, Repository<Archive>>();


    internal static IServiceCollection AddExcel(this IServiceCollection services)
        => services
        .AddTransient<IExcelService, ExcelService>();
}
