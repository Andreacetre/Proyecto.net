using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeoSoft.Models
{
    public class DetalleVenta
    {
        [Key]
        public int DetalleVentaId { get; set; }

        [Required(ErrorMessage = "El ID de la venta es obligatorio.")]
        public int VentaId { get; set; }

        [Required(ErrorMessage = "La venta es obligatoria.")]
        public Venta Venta { get; set; }

        [Required(ErrorMessage = "El ID del producto es obligatorio.")]
        public int ProductoId { get; set; }

        [Required(ErrorMessage = "El producto es obligatorio.")]
        public Producto Producto { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser un valor positivo.")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio.")]
        [Range(0.01, 999999.99, ErrorMessage = "El precio debe ser mayor que 0.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "El subtotal es obligatorio.")]
        [Range(0.01, 9999999.99, ErrorMessage = "El subtotal debe ser mayor que 0.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal SubTotal { get; set; }
    }
}
