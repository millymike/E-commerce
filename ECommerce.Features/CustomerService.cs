using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ECommerce.Models;
using ECommerce.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ECommerce.Features;

public class CustomerService : ICustomerService
{
    private readonly DataContext _dataContext;
    private readonly AppSettings _appSettings;
    
    public CustomerService(
        DataContext dataContext, 
        AppSettings appSettings)
    {
        _dataContext = dataContext;
        _appSettings = appSettings;
    }

    public async Task<Customer> CreateCustomer(Customer customer)
    {
        _dataContext.Customers.Add(customer);
        await _dataContext.SaveChangesAsync();
        return customer;
    }

    public async Task<Customer> UpdateCustomer(Customer customer)
    {
        _dataContext.Customers.Update(customer);
        await _dataContext.SaveChangesAsync();
        return customer;
    }

    public async Task<Customer?> GetCustomerByEmailAddress(string? emailAddress)
    {
        return await _dataContext.Customers.SingleOrDefaultAsync(y => y.EmailAddress == emailAddress);
    }
    
    public async Task<Customer?> GetCustomerById(long customerId)
    {
        return await _dataContext.Customers.SingleOrDefaultAsync();
    }
    
    public async Task<string?> CreatePasswordHash(string? password)
    {
        return await Task.FromResult(BCrypt.Net.BCrypt.HashPassword(password));
    }
    
    public Task<string> CreateJwtToken(Customer customer)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JwtSecret));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var claims = new List<Claim>
            { new("sub", customer.Id.ToString()) };
        
        return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(
            new JwtSecurityToken
            (
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
            )));
    }
    
    public Task<bool> VerifyPassword(string password, Customer customer)
    {
        return Task.FromResult(BCrypt.Net.BCrypt.Verify(password, customer.PasswordHash));
    }
}