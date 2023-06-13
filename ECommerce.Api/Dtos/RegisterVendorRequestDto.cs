using System.ComponentModel.DataAnnotations;

namespace ECommerce.Api.Dtos;

public class RegisterVendorRequestDto
{
    [Required] public string? EmailAddress { get; set; } 
    public string? CompanyName { get; set; } 
    public string? Phone { get; set; }
    public string? Address { get; set; } 
    public string? TaxIdentificationNumber { get; set; } 
    public string? Description { get; set; }
    public string? Password { get; set; } 
}