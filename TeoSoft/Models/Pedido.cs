using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeoSoft.Models
{
    public class Pedido
    {
        [Key]
        public int IdPedido { get; set; }

        [Required]
        public int IdCliente { get; set; }
        public Cliente Cliente { get; set; }

        [Required]
        public int IdProducto { get; set; }
        public Producto Producto { get; set; }

        [Required]
        public string DireccionEnvio { get; set; }

        [Required]
        public string MetodoPago { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal MontoTotal { get; set; }

        [Required]
        public DateTime FechaDelPedido { get; set; }

        [Required]
        public string EstadoPedido { get; set; }

        // Mantenemos la relación con DetallesPedido por si se necesita en el futuro
        public List<DetallePedido> DetallesPedido { get; set; } = new List<DetallePedido>();
    }
}

