using Personal.Project.DatabaseLibrary.Enums;

namespace Personal.Project.DatabaseLibrary.Adapters
{
    #region Class: WindDirectionAdapter
    /// <summary>
    /// Адаптер для направления ветра.
    /// </summary>
    public static class WindDirectionAdapter
    {
        #region Fields: Private
        /// <summary>
        /// Смещение числового значения направления ветра.
        /// </summary>
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
        #endregion

        #region Methods: Internal
        /// <summary>
        /// Конвертирует в числовое представление для хранения значение направления ветра.
        /// </summary>
        /// <param name="windDirection">Направление ветра.</param>
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
                result = _windDirectionsDictionary.GetValueOrDefault((WindDirections)(windDirection));
            }

            return result;
        }
        #endregion

        #region Methods: Private
        private static WindDirections GetEnumWindDirection(string windDirection)
        {
            return _windDirectionsDictionary
                .FirstOrDefault(x => x.Value == windDirection)
                .Key;
        }
        #endregion
    }
    #endregion
}
