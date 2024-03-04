namespace Personal.Project.ValidationLibrary
{
    #region Class: StringValidator
    /// <summary>
    /// Валидирует строку.
    /// </summary>
    public static class StringValidator
    {
        /// <summary>
        /// Проверяет строку на пробелы и пустоту.
        /// </summary>
        /// <param name="str">Проверяемая строка.</param>
        public static void CheckWhitespaceOrNull(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                throw new ArgumentException("Строка не должна быть пустой.", nameof(str));
            }
        }
    }
    #endregion
}
