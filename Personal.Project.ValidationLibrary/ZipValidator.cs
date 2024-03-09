namespace Personal.Project.ValidationLibrary
{
    #region Class: ZipValidator
    /// <summary>
    /// Валидирует архив.
    /// </summary>
    public static class ZipValidator
    {
        #region Fields: Private
        /// <summary>
        /// Допустимые расширения архива.
        /// </summary>
        private static readonly string[] _allowedZipExtensions = { "zip", "7z", "rar" };
        #endregion

        #region Methods: Public
        public static void CheckValidZipFile(string value)
        {
            StringValidator.CheckIsNullOrWhitespace(value);
            CheckZipFileNameExtension(value);

        }


        public static void CheckZipFileNameExtension(string value)
        {
            var lastDotIndex = value.LastIndexOf('.');
            var extension = value.Substring(lastDotIndex).ToLower();

            if (!_allowedZipExtensions.Contains(extension))
            {
                throw new ArgumentException("Данный формат архива не поддерживается.");
            }
        }
        #endregion
    }
    #endregion
}
