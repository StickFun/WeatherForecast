namespace Personal.Project.DatabaseLibrary.Repositories
{
    #region Interface: IRepository
    internal interface IRepository<T> : IDisposable 
        where T : class
    {
        #region Methods
        /// <summary>
        /// Возвращает все записи сущности.
        /// </summary>
        /// <returns>IEnumerable записей в базе данных.</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Возвращает запись по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <returns>Запись сущности.</returns>
        T GetRecord(Guid id);

        /// <summary>
        /// Создает новую запись.
        /// </summary>
        /// <param name="record">Запись.</param>
        void Create(T record);

        /// <summary>
        /// Редактирует существующую запись.
        /// </summary>
        /// <param name="record">Запись.</param>
        void Update(T record);

        /// <summary>
        /// Удаляет запись по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        void Delete(Guid id);

        /// <summary>
        /// Сохраняет изменения.
        /// </summary>
        void Save();
        #endregion
    }
    #endregion
}
