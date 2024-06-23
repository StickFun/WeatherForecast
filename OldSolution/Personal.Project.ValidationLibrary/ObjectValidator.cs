namespace Personal.Project.ValidationLibrary
{
    #region Class: ObjectValidator
    /// <summary>
    /// Валидатор объекта.
    /// </summary>
    /// <typeparam name="T">Тип объекта.</typeparam>
    public static class ObjectValidator<T>
    {
        #region Methods: Public
        /// <summary>
        /// Проверяет ссылка на объект не null.
        /// </summary>
        /// <param name="obj">Проверяемый объект.</param>
        /// <exception cref="ArgumentNullException">Ссылка на объект не может быть null.</exception>
        public static void CheckIsNull(T obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("Ссылка на объект не может быть null.", nameof(obj));
            }
        }
        #endregion
    }
    #endregion
}
