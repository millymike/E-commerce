using ECommerce.Models;
using Microsoft.EntityFrameworkCore;


namespace ECommerce.Persistence;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
   
}