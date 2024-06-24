using Microsoft.EntityFrameworkCore;
using WeatherForecast.Application;
using WeatherForecast.Application.Common;
using WeatherForecast.Domain.Extensions;
using WeatherForecast.ForecastParserService.Extensions;
using WeatherForecast.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration
    .GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<DbContext>(options =>
    options.UseSqlServer(connectionString));

var services = builder.Services
    .AddLogging()
    .AddDomain()
    .AddApplication()
    .AddInfrastructure()
    .AddForecastParserServices()
#if DEBUG
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
#endif
    .AddControllers();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

var context = app.Services.GetRequiredService<WeatherForecastContext>();

app.Run();