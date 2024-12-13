using System;
using System.ComponentModel.DataAnnotations;

namespace TeoSoft.Models
{
    public class Devolucion
    {
        [Key]
        public int DevolucionId { get; set; }

        [Required(ErrorMessage = "El ID de la venta es obligatorio.")]
        [Display(Name = "Venta")]
        public int VentaId { get; set; }

        [Required(ErrorMessage = "La venta es obligatoria.")]
        public Venta Venta { get; set; }

        [Required(ErrorMessage = "El ID del producto es obligatorio.")]
        [Display(Name = "Producto")]
        public int ProductoId { get; set; }

        [Required(ErrorMessage = "El producto es obligatorio.")]
        public Producto Producto { get; set; }

        [Required(ErrorMessage = "La fecha de devolución es obligatoria.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de Devolución")]
        public DateTime FechaDeDevolucion { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser un valor positivo.")]
        [Display(Name = "Cantidad")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "El motivo de devolución es obligatorio.")]
        [StringLength(500, ErrorMessage = "El motivo no puede exceder los 500 caracteres.")]
        [Display(Name = "Motivo de Devolución")]
        public string MotivoDeDevolucion { get; set; }

        [Required(ErrorMessage = "El estado de la devolución es obligatorio.")]
        [RegularExpression("^(Pendiente|Aprobada|Rechazada)$", ErrorMessage = "El estado de devolución debe ser 'Pendiente', 'Aprobada' o 'Rechazada'.")]
        [Display(Name = "Estado de Devolución")]
        public string EstadoDeDevolucion { get; set; }
    }
}