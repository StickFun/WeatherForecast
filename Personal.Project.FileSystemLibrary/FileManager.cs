using Personal.Project.ValidationLibrary;
using System.IO.Compression;

namespace Personal.Project.FileSystemLibrary
{
    #region Class: FileManager
    /// <summary>
    /// Файловый менеджер.
    /// </summary>
    public static class FileManager
    {
        #region Fields: Private
        /// <summary>
        /// Путь до папки с временными файлами.
        /// </summary>
        private const string _tempFilePath = "Temp";
        #endregion

        #region Methods: Public
        /// <summary>
        /// Проверяет существование файла или директория по пути.
        /// </summary>
        /// <param name="path">Путь до файла или директория.</param>
        /// <exception cref="ArgumentException">По заданному пути не найден файл или директорий.</exception>
        public static void CheckPathExists(string path)
        {
            StringValidator.CheckIsNullOrWhitespace(path);

            if (!Path.Exists(path)) 
            {
                throw new ArgumentException("По заданному пути не найден файл или директорий.");
            }
        }

        /// <summary>
        /// Создает директорий для временного хранения файлов и возвращает путь к ней.
        /// </summary>
        /// <param name="path">Путь до каталога, в котором будет создаваться временная папка.</param>
        /// <returns>Путь до директория для временного хранения файлов.</returns>
        public static string CreateTempDirectory(string path)
        {
            StringValidator.CheckIsNullOrWhitespace(path);
            var tempDirectoryPath = $"{path}\\{_tempFilePath}\\{Guid.NewGuid()}";
            
            if (Directory.Exists(tempDirectoryPath))
            {
                Directory.Delete(tempDirectoryPath, true);
            }

            Directory.CreateDirectory(tempDirectoryPath);

            return tempDirectoryPath;
        }

        /// <summary>
        /// Выполняет разархивацию архива по заданному пути.
        /// </summary>
        /// <param name="zipFileStream">Поток архива.</param>
        /// <param name="destinationPath">Путь назначения.</param>
        public static void UnzipTo(Stream zipFileStream, string destinationPath)
        {
            ObjectValidator<Stream>.CheckIsNull(zipFileStream);
            StringValidator.CheckIsNullOrWhitespace(destinationPath);
            CheckPathExists(destinationPath);

            ZipFile.ExtractToDirectory(zipFileStream, destinationPath);
        }
        #endregion
    }
    #endregion
}
