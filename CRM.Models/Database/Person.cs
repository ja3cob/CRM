using System.Text.Json.Serialization;
namespace CRM.Models.Database
{
    public class Person
    {
        public string Username { get; set; }
        [JsonIgnore]
        public string HashedPassword { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Role Role { get; set; }

        public Person(string username, string hashedPassword, string? firstName, string? lastName, Role role)
        {
            Username = username;
            HashedPassword = hashedPassword;
            FirstName = firstName;
            LastName = lastName;
            Role = role;
        }
    }
}
