using ECommerce.Models;
using ECommerce.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Features;

public class UserService : IUserService
{
    private readonly DataContext _dataContext;

    public UserService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<User> CreateUser(User user)
    {
        _dataContext.Users?.Add(user);
        await _dataContext.SaveChangesAsync();
        return user;
    }

    public async Task<User> UpdateUser(User user)
    {
        _dataContext.Users.Update(user);
        await _dataContext.SaveChangesAsync();
        return user;
    }

    public async Task<User?> GetUserByEmailAddress(string? emailAddress)
    {
        return await _dataContext.Users.SingleOrDefaultAsync(y => y.EmailAddress == emailAddress);
    }

    public async Task<User?> GetUserByUsername(string? username)
    {
        return await _dataContext.Users.SingleOrDefaultAsync(y => y.Username == username);
    }

    public async Task<User?> GetUserById(long userId)
    {
        return await _dataContext.Users.SingleOrDefaultAsync();
    }
    
    public async Task<string?> CreatePasswordHash(string? password)
    {
        return await Task.FromResult(BCrypt.Net.BCrypt.HashPassword(password));
    }
    
}