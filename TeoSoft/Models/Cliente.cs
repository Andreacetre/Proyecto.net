using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeoSoft.Models
{
    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria.")]
        [StringLength(200, ErrorMessage = "La dirección no puede exceder los 200 caracteres.")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [Phone(ErrorMessage = "El teléfono debe tener un formato válido.")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El documento es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El número de documento debe ser un valor positivo.")]
        public int Documento { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo electrónico no tiene un formato válido.")]
        public string Correo { get; set; }

        public ICollection<Venta> Ventas { get; set; }

        public ICollection<Pedido> Pedidos { get; set; }
    }
}