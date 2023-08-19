using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace CRM.Models
{
    public class Person
    {
        [JsonIgnore]
        public int? Id { get; set; }
        public string Username { get; set; } = null!;
        [JsonIgnore]
        public string? Password { get; set; }
        [NotMapped]
        [JsonPropertyName(nameof(Password))]
        public string? PasswordJson 
        {
            get => null;
            set => Password = value;
        }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Role Role { get; set; }
    }
}
