using WeatherForecast.Application.Enums;

namespace WeatherForecast.Infrastructure;

public static class WindDirectionAdapter
{
    private const int _digitWindDirectionOffset = 10;
    private static Dictionary<WindDirections, string> _windDirectionsDictionary = new Dictionary<WindDirections, string>()
    {
        {WindDirections.Calm, "штиль" },
        {WindDirections.NorthWest, "СЗ" },
        {WindDirections.North, "С" },
        {WindDirections.NorthEast, "СВ" },
        {WindDirections.SouthEast, "ЮВ" },
        {WindDirections.South, "Ю" },
        {WindDirections.SouthWest, "ЮЗ" },
        {WindDirections.West, "З" },
    };

    public static int ConvertToIntValue(string windDirection)
    {
        int convertedWindDirection;
        if (windDirection.Contains(',')) 
        { 
            var directions = windDirection.Split(',');
            convertedWindDirection = (int)GetEnumWindDirection(directions[0]);
            convertedWindDirection = convertedWindDirection * _digitWindDirectionOffset + (int)GetEnumWindDirection(directions[1]);
        }
        else
        {
            convertedWindDirection = (int)_windDirectionsDictionary
                .FirstOrDefault(x => x.Value == windDirection)
                .Key;
        }

        return convertedWindDirection;
    }

    public static string ConvertToStringValue(int windDirection)
    {
        string result;

        if (windDirection > _digitWindDirectionOffset)
        {
            result = $"{_windDirectionsDictionary.GetValueOrDefault((WindDirections)(windDirection/_digitWindDirectionOffset))}," +
                $"{_windDirectionsDictionary.GetValueOrDefault((WindDirections)(windDirection%_digitWindDirectionOffset))}";
        }
        else
        {
            result = _windDirectionsDictionary.GetValueOrDefault((WindDirections)windDirection);
        }

        return result;
    }

    private static WindDirections GetEnumWindDirection(string windDirection)
    {
        return _windDirectionsDictionary
            .FirstOrDefault(x => x.Value == windDirection)
            .Key;
    }
}
