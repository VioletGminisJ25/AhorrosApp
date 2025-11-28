using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
namespace AhorrosApp.Models
{
    [Table("Categorias")]
    public class Categoria
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        
        public string? Nombre {  get; set; }
    }
}
