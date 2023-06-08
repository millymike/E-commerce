using System.ComponentModel.DataAnnotations;

namespace ECommerce.Api.Dtos;

public class RegisterUserRequestDto
{
    [Required] public string? EmailAddress { get; set; } 
    public string? FirstName { get; set; } 
    public string? LastName { get; set; }
    public string? Username { get; set; } 
    public string? Password { get; set; } 
}