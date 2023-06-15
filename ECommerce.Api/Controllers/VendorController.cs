using ECommerce.Api.Dtos;
using ECommerce.Features;
using ECommerce.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
[ProducesResponseType(typeof(UserInputErrorDto), 400)]
public class VendorController : AbstractController
{
    private readonly IValidator<RegisterVendorRequestDto> _registerVendorRequest;
    private readonly IValidator<LoginVendorRequestDto> _loginVendorRequest;
    private readonly IVendorService _vendorService;

    public VendorController(
        IVendorService vendorService, 
        IValidator<RegisterVendorRequestDto> registerVendorRequest, 
        IValidator<LoginVendorRequestDto> loginVendorRequest)
    {
        _vendorService = vendorService;
        _registerVendorRequest = registerVendorRequest;
        _loginVendorRequest = loginVendorRequest;
    }

    [HttpPost]
    [ProducesResponseType(typeof(EmptySuccessResponseDto), 200)]
    public async Task<ActionResult<EmptySuccessResponseDto>> Register(RegisterVendorRequestDto requestDto)
    {
        var result = await _registerVendorRequest.ValidateAsync(requestDto);
        
        if (!result.IsValid) return BadRequest(new UserInputErrorDto(result));
        
        var passwordHash = _vendorService.CreatePasswordHash(requestDto.Password);
        var vendor = await _vendorService.GetVendorByEmailAddress(requestDto.EmailAddress);
        var companyName = await  _vendorService.GetVendorByCompanyName(requestDto.CompanyName);
        
        if(vendor != null || requestDto.CompanyName == companyName?.CompanyName)
            return BadRequest("Vendor with Email/CompanyName already exists");
        
        await _vendorService.CreateVendor(new Vendor
        {
            EmailAddress = requestDto.EmailAddress,
            CompanyName = requestDto.CompanyName,
            Phone = requestDto.Phone,
            Address = requestDto.Address,
            TaxIdentificationNumber = requestDto.TaxIdentificationNumber,
            Description = requestDto.Description,
            PasswordHash = await passwordHash,
            CreatedAt = DateTime.UtcNow
        });

        return Ok(new EmptySuccessResponseDto("Registration Successful!"));
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(EmptySuccessResponseDto), 200)]
    public async Task<ActionResult<JwtDto>> Login(LoginVendorRequestDto loginVendorRequestDto)
    {
        var result = await _loginVendorRequest.ValidateAsync(loginVendorRequestDto);
        if (!result.IsValid) return BadRequest(new UserInputErrorDto(result));
        
        var vendor = await _vendorService.GetVendorByEmailAddress(loginVendorRequestDto.EmailAddress);
        if (vendor == null || !await _vendorService.VerifyPassword(loginVendorRequestDto.Password, vendor)) 
            return BadRequest(new UserInputErrorDto("Incorrect email/password!"));

        vendor.LastLogin = DateTime.UtcNow;
        await _vendorService.UpdateVendor(vendor);

        return Ok(new JwtDto
            (new JwtDto.Credentials { AccessToken = await _vendorService.CreateJwtToken(vendor) }));
    }
}