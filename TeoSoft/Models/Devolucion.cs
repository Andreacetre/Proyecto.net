using System;
using System.ComponentModel.DataAnnotations;

namespace TeoSoft.Models
{
    public class Devolucion
    {
        [Key]
        public int IdDevolucion { get; set; }

        public int VentaId { get; set; }
        public Venta Venta { get; set; }

        public int ProductoId { get; set; }
        public Producto Producto { get; set; }

        public DateTime FechaDeDevolucion { get; set; }

        public int Cantidad { get; set; }

        public string MotivoDeDevolucion { get; set; }

        public string EstadoDeDevolucion { get; set; }
    }
}

