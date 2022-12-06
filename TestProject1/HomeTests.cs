using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApplication3.Controllers;

namespace TestProject1;

public class HomeTests
{
    [SetUp]
    public void Setup()
    {
        var homeController = new HomeController(new Logger<HomeController>(new LoggerFactory()),);
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }
    
    
}