using System.Text.Json.Serialization;

namespace ECommerce.Models;

public class Customer
{
    [JsonIgnore] public long Id { get; set; }
    [JsonIgnore] public string? EmailAddress { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    [JsonIgnore] public string? PasswordHash { get; set; }
    [JsonIgnore] public DateTime LastLogin { get; set; }
    [JsonIgnore] public DateTime CreatedAt { get; set; }
}