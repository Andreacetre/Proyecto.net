using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TeoSoft.Models
{
    public class User
    {
        [JsonPropertyName("_id")]
        public string Id { get; set; }

        [JsonPropertyName("nombre")]
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres.")]
        public string FirstName { get; set; }

        [JsonPropertyName("apellido")]
        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(50, ErrorMessage = "El apellido no puede exceder los 50 caracteres.")]
        public string LastName { get; set; }

        [JsonPropertyName("email")]
        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "Debe ser una dirección de correo válida.")]
        public string Email { get; set; }

        [JsonPropertyName("contrasena")]
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener entre 6 y 100 caracteres.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [JsonPropertyName("rol")]
        [Required(ErrorMessage = "El rol es obligatorio.")]
        [RegularExpression("^(admin|usuario|invitado)$", ErrorMessage = "El rol debe ser admin, usuario o invitado.")]
        public string Role { get; set; }

        [JsonPropertyName("estado")]
        [Required(ErrorMessage = "El estado es obligatorio.")]
        [RegularExpression("^(activo|inactivo)$", ErrorMessage = "El estado debe ser activo o inactivo.")]
        public string Status { get; set; }
    }
}
