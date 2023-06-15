using ECommerce.Api.Dtos;
using ECommerce.Features;
using ECommerce.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;


[Route("api/[controller]/[action]")]
[ApiController]
[ProducesResponseType(typeof(UserInputErrorDto), 400)]

public class CustomerController : AbstractController
{
    private readonly IValidator<RegisterCustomerRequestDto> _registerCustomerRequest;
    private readonly IValidator<LoginCustomerRequestDto> _loginCustomerRequest;
    private readonly ICustomerService _customerService;

    public CustomerController(
        IValidator<RegisterCustomerRequestDto> registerCustomerRequest, 
        IValidator<LoginCustomerRequestDto> loginCustomerRequest, 
        ICustomerService customerService)
    {
        _registerCustomerRequest = registerCustomerRequest;
        _loginCustomerRequest = loginCustomerRequest;
        _customerService = customerService;
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(EmptySuccessResponseDto), 200)]
    public async Task<ActionResult<EmptySuccessResponseDto>> Register(RegisterCustomerRequestDto requestDto)
    {
        var result = await _registerCustomerRequest.ValidateAsync(requestDto);
        
        if (!result.IsValid) return BadRequest(new UserInputErrorDto(result));
        
        var passwordHash = _customerService.CreatePasswordHash(requestDto.Password);
        var customer = await _customerService.GetCustomerByEmailAddress(requestDto.EmailAddress);
        
        if(customer != null)
            return BadRequest("Customer with Email already exists");
        
        await _customerService.CreateCustomer(new Customer
        {
            EmailAddress = requestDto.EmailAddress,
            FirstName = requestDto.FirstName,
            LastName = requestDto.LastName,
            PasswordHash = await passwordHash,
            CreatedAt = DateTime.UtcNow
        });

        return Ok(new EmptySuccessResponseDto("Registration Successful!"));
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(EmptySuccessResponseDto), 200)]
    public async Task<ActionResult<JwtDto>> Login(LoginCustomerRequestDto loginCustomerRequestDto)
    {
        var result = await _loginCustomerRequest.ValidateAsync(loginCustomerRequestDto);
        if (!result.IsValid) return BadRequest(new UserInputErrorDto(result));
        
        var customer = await _customerService.GetCustomerByEmailAddress(loginCustomerRequestDto.EmailAddress);
        if (customer == null || !await _customerService.VerifyPassword(loginCustomerRequestDto.Password, customer)) 
            return BadRequest(new UserInputErrorDto("Incorrect email/password!"));

        customer.LastLogin = DateTime.UtcNow;
        await _customerService.UpdateCustomer(customer);

        return Ok(new JwtDto
            (new JwtDto.Credentials { AccessToken = await _customerService.CreateJwtToken(customer) }));
    }
}