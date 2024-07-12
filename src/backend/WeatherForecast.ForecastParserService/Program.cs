using Microsoft.EntityFrameworkCore;
using WeatherForecast.Application;
using WeatherForecast.Domain.Extensions;
using WeatherForecast.ForecastParserService.Extensions;
using WeatherForecast.Infrastructure;
using WeatherForecast.Infrastructure.Database.Contexts;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration
    .GetConnectionString("DefaultConnection");

var services = builder.Services
    .AddLogging()
    .AddDbContext<ArchiveContext>(options =>
        options.UseSqlServer(connectionString))
    .AddDbContext<ForecastContext>(options =>
        options.UseSqlServer(connectionString))
    .AddInfrastructure()
    .AddApplication()
    .AddForecastParserServices()
    .AddDomain()
#if DEBUG
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
#endif
    .AddControllers();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    //app.UseHsts();
}
else if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

app.Run();