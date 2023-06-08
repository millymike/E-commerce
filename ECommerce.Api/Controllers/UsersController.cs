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
    public async Task<ActionResult<EmptySuccessResponseDto>> Register(RegisterUserRequestDto requestDto)
    {
        
         await _userService.CreateUser(new User
         {
             EmailAddress = requestDto.EmailAddress,
             Username = requestDto.Username,
             FirstName = requestDto.FirstName,
             LastName = requestDto.LastName,
             PasswordHash = requestDto.Password,
             CreatedAt = DateTime.UtcNow
         });

        return Ok(new EmptySuccessResponseDto("Registration Successful!"));
    }
}