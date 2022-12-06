using System.Data.Entity;
using WebApplication3.Data;
using WebApplication3.Models;

namespace TestProject1;

public class TestApplicationDbContext : IApplicationDbContext
{
    public TestApplicationDbContext()
    {
        this.Categories = new TestCategoryDdSet();
    }
    public IDbSet<Category> Categories { get; }
    public int SaveChanges()
    {
        return 0;
    }

    public Microsoft.EntityFrameworkCore.DbSet<UserAuthentication> UserAuthentications { get; }
}