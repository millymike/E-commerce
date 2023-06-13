using System.Text.Json.Serialization;

namespace ECommerce.Models;

public class Vendor
{
    [JsonIgnore] public long Id { get; set; }
    [JsonIgnore] public string? EmailAddress { get; set; }
    public string? CompanyName { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? TaxIdentificationNumber { get; set; }
    public string? Description { get; set; }
    [JsonIgnore] public string? PasswordHash { get; set; }
    [JsonIgnore] public DateTime LastLogin { get; set; }
    [JsonIgnore] public DateTime CreatedAt { get; set; }
}