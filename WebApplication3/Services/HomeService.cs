using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Services;

public class HomeService : IHomeService
{
    private readonly ApplicationDbContext _db;
    public HomeService(ApplicationDbContext db)
    {
        _db = db;
    }

    public void AddUserDetailsToDb(UserAuthentication userAuthentication)
    {
        _db.UserAuthentications.Add(userAuthentication);
        _db.SaveChanges();
    }
    
    public UserAuthentication FindUserCredentials(UserAuthentication obj)
    {
        return _db.UserAuthentications.Find(obj.UserName)!;
    }
    
}