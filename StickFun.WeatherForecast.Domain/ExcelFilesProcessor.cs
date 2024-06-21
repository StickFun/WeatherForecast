using StickFun.WeatherForecast.Domain.Common;
using StickFun.WeatherForecast.Domain.Exceptions;

namespace StickFun.WeatherForecast.Domain;

public class ExcelFilesProcessor(
    IParserContext parserContext, 
    IFileManager fileManager)
{
    public async Task ParseWeatherForecastFromExcelFiles()
    {
        var directoryPath = await parserContext.GetExcelFilesDirectoryPath();

        if (!await fileManager.IsDirectoryExists(directoryPath))
            throw new DomainException($"Отсутствует директорий по пути '{directoryPath}'.");

        foreach(var excelFile in await fileManager.GetExcelFiles(directoryPath))
        {
            try
            {
                excelFile.Process();
            }
            catch
            {
                // todo: добавить обработку ошибки.
            }
        }
    }
}
