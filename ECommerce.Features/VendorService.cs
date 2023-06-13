using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ECommerce.Models;
using ECommerce.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ECommerce.Features;

public class VendorService : IVendorService
{
    private readonly DataContext _dataContext;
    private readonly AppSettings _appSettings;
    
    public VendorService(
        DataContext dataContext, 
        AppSettings appSettings)
    {
        _dataContext = dataContext;
        _appSettings = appSettings;
    }

    public async Task<Vendor> CreateVendor(Vendor vendor)
    {
        _dataContext.Vendors.Add(vendor);
        await _dataContext.SaveChangesAsync();
        return vendor;
    }

    public async Task<Vendor> UpdateVendor(Vendor vendor)
    {
        _dataContext.Vendors.Update(vendor);
        await _dataContext.SaveChangesAsync();
        return vendor;
    }

    public async Task<Vendor?> GetVendorByEmailAddress(string? emailAddress)
    {
        return await _dataContext.Vendors.SingleOrDefaultAsync(y => y.EmailAddress == emailAddress);
    }

    public async Task<Vendor?> GetVendorByCompanyName(string? companyName)
    {
        return await _dataContext.Vendors.SingleOrDefaultAsync(y => y.CompanyName == companyName);
    }

    public async Task<Vendor?> GetVendorById(long vendorId)
    {
        return await _dataContext.Vendors.SingleOrDefaultAsync();
    }
    
    public async Task<string?> CreatePasswordHash(string? password)
    {
        return await Task.FromResult(BCrypt.Net.BCrypt.HashPassword(password));
    }
    public Task<string> CreateJwtToken(Vendor vendor)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JwtSecret));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var claims = new List<Claim>
            { new("sub", vendor.Id.ToString()) };
        
        return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(
            new JwtSecurityToken
            (
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
            )));
    }
    
    public Task<bool> VerifyPassword(string password, Vendor vendor)
    {
        return Task.FromResult(BCrypt.Net.BCrypt.Verify(password, vendor.PasswordHash));
    }
    
}