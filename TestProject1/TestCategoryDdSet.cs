using System.Data.Entity;
using WebApplication3.Models;

namespace TestProject1;

public class TestCategoryDdSet : TestDbSet<Category>
{
    public override Category Find(params object[] keyValues)
    {
        return this.SingleOrDefault(product => product.Id == (int)keyValues.Single());
    }
    
}