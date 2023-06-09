using ECommerce.Models;

namespace ECommerce.Features;

public interface IUserService
{
    public Task<User> CreateUser(User user);
    public Task<string?> CreatePasswordHash(string? password);

}