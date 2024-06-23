using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Personal.Project.DatabaseLibrary.Entities
{
    #region Class: Archive
    /// <summary>
    /// Сущность архива погоды.
    /// </summary>
    [Table("Archive")]
    public class Archive
    {
        #region Properties: Public
        /// <summary>
        /// Идентификатор.
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Название архива.
        /// </summary>'
        [Column("Name")]
        public string? Name { get; set; }
        #endregion
    }
    #endregion
}
