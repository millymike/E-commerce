using ECommerce.Api.Dtos;
using ECommerce.Features;
using ECommerce.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
[ProducesResponseType(typeof(UserInputErrorDto), 400)]
public class UsersController : AbstractController
{
    private readonly IValidator<RegisterUserRequestDto> _registerUserRequest;
    private readonly IUserService _userService;

    public UsersController(IUserService userService, IValidator<RegisterUserRequestDto> registerUserRequest)
    {
        _userService = userService;
        _registerUserRequest = registerUserRequest;
    }

    [HttpPost]
    [ProducesResponseType(typeof(EmptySuccessResponseDto), 200)]
    public async Task<ActionResult<EmptySuccessResponseDto>> Register(RegisterUserRequestDto requestDto)
    {
        var result = await _registerUserRequest.ValidateAsync(requestDto);
        
        if (!result.IsValid) return BadRequest(new UserInputErrorDto(result));
        
        
        var passwordHash = _userService.CreatePasswordHash(requestDto.Password);
        var user = await _userService.GetUserByEmailAddress(requestDto.EmailAddress);
        var username = await  _userService.GetUserByUsername(requestDto.Username);
        
        if(user != null || requestDto.Username == username?.Username)
            return BadRequest("User with email/username already exists");
        
        await _userService.CreateUser(new User
        {
            EmailAddress = requestDto.EmailAddress,
            Username = requestDto.Username,
            FirstName = requestDto.FirstName,
            LastName = requestDto.LastName,
            PasswordHash = await passwordHash,
            CreatedAt = DateTime.UtcNow
        });

        return Ok(new EmptySuccessResponseDto("Registration Successful!"));
    }
}