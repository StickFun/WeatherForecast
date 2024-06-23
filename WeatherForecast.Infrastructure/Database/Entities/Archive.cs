using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherForecast.Infrastructure.Database.Entities
{
    [Table("Archive")]
    public class Archive : IBaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Column("Name")]
        public string? Name { get; set; }
    }
}
