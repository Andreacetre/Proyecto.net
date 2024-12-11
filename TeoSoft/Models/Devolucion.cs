using System;
using System.ComponentModel.DataAnnotations;

namespace TeoSoft.Models
{
    public class Devolucion
    {
        [Key]
        public int IdDevolucion { get; set; }

        [Required(ErrorMessage = "El ID de la venta es obligatorio.")]
        public int VentaId { get; set; }

        [Required(ErrorMessage = "La venta es obligatoria.")]
        public Venta Venta { get; set; }

        [Required(ErrorMessage = "El ID del producto es obligatorio.")]
        public int ProductoId { get; set; }

        [Required(ErrorMessage = "El producto es obligatorio.")]
        public Producto Producto { get; set; }

        [Required(ErrorMessage = "La fecha de devolución es obligatoria.")]
        public DateTime FechaDeDevolucion { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser un valor positivo.")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "El motivo de devolución es obligatorio.")]
        [StringLength(500, ErrorMessage = "El motivo no puede exceder los 500 caracteres.")]
        public string MotivoDeDevolucion { get; set; }

        [Required(ErrorMessage = "El estado de la devolución es obligatorio.")]
        [RegularExpression("^(pendiente|aprobada|rechazada)$", ErrorMessage = "El estado de devolución debe ser 'pendiente', 'aprobada' o 'rechazada'.")]
        public string EstadoDeDevolucion { get; set; }
    }
}
