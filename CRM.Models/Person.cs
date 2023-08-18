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
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Role Role { get; set; }
    }
}
