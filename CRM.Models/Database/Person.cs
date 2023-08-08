using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace CRM.Models.Database
{
    public class Person
    {
        [Key]
        public string Username { get; set; } = null!;
        [JsonIgnore]
        public string HashedPassword { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Role Role { get; set; }
    }
}
