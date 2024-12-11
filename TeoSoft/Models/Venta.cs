using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeoSoft.Models
{
    public class Venta
    {
        [Key]
        public int VentaId { get; set; }

        public DateTime Fecha { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }

        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        [Required]
        public int IdProducto { get; set; }
        public Producto Producto { get; set; }

        public string Estado { get; set; }

        // Mantenemos estas relaciones por si se necesitan en el futuro
        public List<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();
        public ICollection<Devolucion> Devoluciones { get; set; }
    }
}

