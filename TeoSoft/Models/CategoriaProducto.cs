using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeoSoft.Models
{
    public class CategoriaProducto
    {
        [Key]
        public int IdCatProduc { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public string Estado { get; set; }

        public ICollection<Producto> Productos { get; set; }
    }
}

