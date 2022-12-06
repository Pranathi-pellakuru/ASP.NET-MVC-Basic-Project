using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebApplication3.Models;

namespace TestProject1;

public class TestUserAuthenticationsDbSet : TestDbSet<UserAuthentication>
{
    
    public override UserAuthentication Find(params object[] keyValues)
    {
        return this.SingleOrDefault(product => product.UserName == keyValues.Single());
    }
}