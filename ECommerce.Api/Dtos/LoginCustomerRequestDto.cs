using System.ComponentModel.DataAnnotations;

namespace ECommerce.Api.Dtos;

public class LoginCustomerRequestDto
{
    [Required] public string EmailAddress { get; set; }
    [Required] public string Password { get; set; }
}