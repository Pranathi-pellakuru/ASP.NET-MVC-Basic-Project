using Microsoft.EntityFrameworkCore;
using WebApplication3.Models;

namespace WebApplication3.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Category> Categories { get; set; }
    public  DbSet<UserAuthentication> UserAuthentications { get; set; }
}