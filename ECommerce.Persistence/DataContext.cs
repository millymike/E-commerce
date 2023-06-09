using ECommerce.Models;
using Microsoft.EntityFrameworkCore;


namespace ECommerce.Persistence;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }
    public DbSet<Vendor> Vendors { get; set; }
    
    public DbSet<Customer> Customers { get; set; }

}