using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLiteNetExtensions.Attributes;
using AhorrosApp.Models;
namespace AhorrosApp.Models
{
    [Table("Gastos")]
    public class Gasto
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }


        public decimal Importe { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }

        [ForeignKey(typeof(Categoria))]
        public int CategoriaId { get; set; }

        [Ignore]
        public string NombreCategoria { get; set; }

    }
}
