using ECommerce.Models;
using ECommerce.Persistence;

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
}