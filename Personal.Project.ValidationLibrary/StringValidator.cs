namespace Personal.Project.ValidationLibrary
{
    #region Class: StringValidator
    /// <summary>
    /// Валидирует строку.
    /// </summary>
    public static class StringValidator
    {
        #region Methods: Public
        /// <summary>
        /// Проверяет, что строка не пустая и не состоит и пробелов.
        /// </summary>
        /// <param name="value">Проверяемая строка.</param>
        /// <exception cref="ArgumentNullException">Строка не может быть пустой.</exception>
        public static void CheckIsNullOrWhitespace(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException("Строка не может быть пустой", nameof(value));
            }
        }
        #endregion
    }
    #endregion
}
