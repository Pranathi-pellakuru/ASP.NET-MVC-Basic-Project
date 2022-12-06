using WebApplication3.Models;

namespace WebApplication3.Services;

public interface IHomeService
{
    void AddUserDetailsToDb(UserAuthentication userAuthentication);
    UserAuthentication FindUserCredentials(UserAuthentication obj);
}