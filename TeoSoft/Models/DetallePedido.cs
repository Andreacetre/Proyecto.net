using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeoSoft.Models
{
    public class DetallePedido
    {
        [Key]
        public int IdDetPedido { get; set; }

        [Required(ErrorMessage = "El ID del pedido es obligatorio.")]
        public int IdPedido { get; set; }

        [Required(ErrorMessage = "El pedido es obligatorio.")]
        public Pedido Pedido { get; set; }

        [Required(ErrorMessage = "El ID del producto es obligatorio.")]
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "El producto es obligatorio.")]
        public Producto Producto { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser un valor positivo.")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "El precio unitario es obligatorio.")]
        [Range(0.01, 999999.99, ErrorMessage = "El precio unitario debe ser mayor que 0.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecioUnitario { get; set; }

        // Subtotal se calcula como cantidad * precio unitario, no es necesario que se almacene, se puede usar una propiedad calculada
        [Column(TypeName = "decimal(18,2)")]
        public decimal Subtotal => Cantidad * PrecioUnitario;
    }
}
