using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeoSoft.Models
{
    public class DetallePedido
    {
        [Key]
        public int IdDetPedido { get; set; }

        [Required]
        public int IdPedido { get; set; }
        public Pedido Pedido { get; set; }

        [Required]
        public int IdProducto { get; set; }
        public Producto Producto { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecioUnitario { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Subtotal => Cantidad * PrecioUnitario;

        public decimal subtotal { get; internal set; }
        public int PedidoId { get; internal set; }
        public int ProductoId { get; internal set; }
    }
}

