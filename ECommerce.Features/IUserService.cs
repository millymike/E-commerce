using ECommerce.Models;

namespace ECommerce.Features;

public interface IUserService
{
    public Task<User> CreateUser(User user);
    public Task<User> UpdateUser(User user);
    public Task<string?> CreatePasswordHash(string? password);
    public Task<User?> GetUserByEmailAddress(string? emailAddress);
    public Task<User?> GetUserByUsername(string? username);
    public Task<User?> GetUserById(long userId);
    
}