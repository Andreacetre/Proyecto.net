using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeoSoft.Models
{
    public class Producto
    {
        [Key]
        public int ProductoId { get; set; }

        public string CodigoDeBarra { get; set; }

        public string Nombre { get; set; }

        public decimal Precio { get; set; }

        public int Stock { get; set; }

        public string Descripcion { get; set; }

        public double IVA { get; set; }

        public DateTime? FechaVencimiento { get; set; }

        public bool SinVencimiento { get; set; }

        public string Estado { get; set; } = "Activo";

        public int CategoriaProductoId { get; set; }
        public CategoriaProducto CategoriaProducto { get; set; }

        // Add these properties
        public ICollection<DetalleVenta> DetalleVentas { get; set; }
        public ICollection<DetallePedido> DetallePedidos { get; set; }
        public ICollection<Devolucion> Devoluciones { get; set; }
    }
}

