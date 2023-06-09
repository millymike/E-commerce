using ECommerce.Api.Dtos;
using ECommerce.Features;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class UsersController : AbstractController
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(EmptySuccessResponseDto), 200)]
    public async Task<ActionResult<EmptySuccessResponseDto>> Register(RegisterUserRequestDto requestDto)
    {
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