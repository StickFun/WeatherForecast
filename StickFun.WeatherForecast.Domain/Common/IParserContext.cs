namespace StickFun.WeatherForecast.Domain.Common;

public interface IParserContext
{
    Task<string> GetExcelFilesDirectoryPath();
}
