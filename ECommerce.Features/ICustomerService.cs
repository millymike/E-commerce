using ECommerce.Models;

namespace ECommerce.Features;

public interface ICustomerService
{
    public Task<Customer> CreateCustomer(Customer customer);
    public Task<Customer> UpdateCustomer(Customer customer);
    public Task<Customer?> GetCustomerByEmailAddress(string? emailAddress);
    public Task<Customer?> GetCustomerById(long userId);
    public Task<string?> CreatePasswordHash(string? password);
    public Task<string> CreateJwtToken(Customer customer);
    public Task<bool> VerifyPassword(string password, Customer customer);
}