using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherForecast.Infrastructure
{
    [Table("Archive")]
    public class Archive
    {
        [Key]
        public Guid Id { get; set; }

        [Column("Name")]
        public string? Name { get; set; }
    }
}
