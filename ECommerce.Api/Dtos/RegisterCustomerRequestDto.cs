using System.ComponentModel.DataAnnotations;

namespace ECommerce.Api.Dtos;

public class RegisterCustomerRequestDto
{
    [Required] public string? EmailAddress { get; set; } 
    public string? FirstName { get; set; } 
    public string? LastName { get; set; }
    public string? Password { get; set; } 
}