using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeoSoft.Models
{
    public class CategoriaProducto
    {
        [Key]
        public int IdCatProduc { get; set; }

        [Required(ErrorMessage = "El nombre de la categoría es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre de la categoría no puede exceder los 100 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La descripción de la categoría es obligatoria.")]
        [StringLength(500, ErrorMessage = "La descripción no puede exceder los 500 caracteres.")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio.")]
        [StringLength(20, ErrorMessage = "El estado no puede exceder los 20 caracteres.")]
        public string Estado { get; set; }

        public ICollection<Producto> Productos { get; set; }
    }
}
