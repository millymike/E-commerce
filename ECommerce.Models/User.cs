using System.Text.Json.Serialization;

namespace ECommerce.Models;

public class User
{
    [JsonIgnore] public long Id { get; set; }
    public string? Username { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    [JsonIgnore] public string? EmailAddress { get; set; }
    [JsonIgnore] public string? PasswordHash { get; set; }
    [JsonIgnore] public DateTime CreatedAt { get; set; }
}