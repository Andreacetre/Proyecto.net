using System.Text.Json.Serialization;

namespace TeoSoft.Models
{
    public class User
    {
        [JsonPropertyName("_id")]
        public string Id { get; set; }

        [JsonPropertyName("nombre")]
        public string FirstName { get; set; }

        [JsonPropertyName("apellido")]
        public string LastName { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("contrasena")]
        public string Password { get; set; }

        [JsonPropertyName("rol")]
        public string Role { get; set; }

        [JsonPropertyName("estado")]
        public string Status { get; set; }
    }
}