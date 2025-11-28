
using SQLite;

namespace AhorrosApp.Models
{
    [Table("ObjetivosAhorro")]
    public class ObjetivoAhorro
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string? Descripcion { get; set; }
        public decimal MontoObjetivo { get; set; }
        public decimal MontoActual { get; set; }
        public DateTime FechaLimite { get; set; }
    }
}
