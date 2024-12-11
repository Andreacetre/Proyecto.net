using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeoSoft.Models
{
    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }

        public string Nombre { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }

        public int Documento { get; set; }

        public string Correo { get; set; }

        public ICollection<Venta> Ventas { get; set; }

        public ICollection<Pedido> Pedidos { get; set; }
    }
}

