using ECommerce.Models;

namespace ECommerce.Features;

public interface IVendorService
{
    public Task<Vendor> CreateVendor(Vendor vendor);
    public Task<Vendor> UpdateVendor(Vendor vendor);
    public Task<Vendor?> GetVendorByEmailAddress(string? emailAddress);
    public Task<Vendor?> GetVendorByCompanyName(string? companyName);
    public Task<Vendor?> GetVendorById(long userId);
    public Task<string?> CreatePasswordHash(string? password);
    public Task<string> CreateJwtToken(Vendor vendor);
    public Task<bool> VerifyPassword(string password, Vendor vendor);
    
}