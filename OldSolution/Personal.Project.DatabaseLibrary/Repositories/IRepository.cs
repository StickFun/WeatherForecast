namespace Personal.Project.DatabaseLibrary.Repositories
{
    #region Interface: IRepository
    internal interface IRepository<TEntity> : IDisposable 
        where TEntity : class
    {
        #region Methods
        /// <summary>
        /// Возвращает все записи сущности.
        /// </summary>
        /// <returns>IEnumerable записей в базе данных.</returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Возвращает запись по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <returns>Запись сущности.</returns>
        TEntity GetRecord(Guid id);

        /// <summary>
        /// Создает новую запись.
        /// </summary>
        /// <param name="record">Запись.</param>
        void Create(TEntity record);

        /// <summary>
        /// Редактирует существующую запись.
        /// </summary>
        /// <param name="record">Запись.</param>
        void Update(TEntity record);

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
