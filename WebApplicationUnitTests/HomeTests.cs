using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using WebApplication3.Controllers;
using WebApplication3.Models;
using WebApplication3.Services;

namespace WebApplicationUnitTests;

public class HomeTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Register_WhenCalled_RegisterViewShouldRender()
    {
        var service = new Mock<IHomeService>();
        var controller = new HomeController(service.Object);
        
        var result = controller.Register() as ViewResult;
        
        Assert.That(result?.ViewName, Is.EqualTo("Register"));
    }
    
    [Test]
    public void RegisterPOST_WhenCalled_NewUserDetailsAreAddedToDB()
    {
        var service = new Mock<IHomeService>();
        var controller = new HomeController(service.Object);
        var userAuthentication = new UserAuthentication();
        
        controller.Register(userAuthentication);
        
        service.Verify(s => s.AddUserDetailsToDb(userAuthentication));
    }
    
    [Test]
    public void RegisterPOST_WhenCalled_RedirectToTheMainUrl()
    {
        var service = new Mock<IHomeService>();
        var controller = new HomeController(service.Object);
        var userAuthentication = new UserAuthentication();
        
        var result = controller.Register(userAuthentication) as RedirectResult;
        Assert.That(result.Url, Is.EqualTo("/login?ReturnUrl=/"));
    }
    
    [Test]
    public void Login_WhenCalled_LoginViewShouldRender()
    {
        var service = new Mock<IHomeService>();
        var controller = new HomeController(service.Object);
        
        var result = controller.Login("/") as ViewResult;
        
        Assert.That(result?.ViewName, Is.EqualTo("Login"));
    }
    
    [Test]
    public void Login_WhenCalledWithAUrl_ViewDataShouldStorePassedURLAsReturnURL()
    {
        var service = new Mock<IHomeService>();
        var controller = new HomeController(service.Object);
        
        var result = controller.Login("/hello") as ViewResult;
        
        Assert.That(result.ViewData["ReturnUrl"], Is.EqualTo("/hello"));
    }
    
    
    [Test]
    public void Login_WhenCalledWithNull_ViewDataShouldStoreDefaultUrlAsReturnURL()
    {
        var service = new Mock<IHomeService>();
        var controller = new HomeController(service.Object);
        
        var result = controller.Login(null) as ViewResult;
        
        Assert.That(result.ViewData["ReturnUrl"], Is.EqualTo("/"));
    }
    [Test]
    public async Task LoginPOST_WhenCalledSuccess_RedirectToReturnURL()
    {
        var service = new Mock<IHomeService>();
       
        var userAuthentication = new UserAuthentication()
        {
            UserName = "Pra",
            Password = "123"
        };

        service.Setup(s => s.FindUserCredentials(userAuthentication)).Returns(userAuthentication);
        var authServiceMock = new Mock<IAuthenticationService>();
        authServiceMock
            .Setup(_ => _.SignInAsync(It.IsAny<HttpContext>(), It.IsAny<string>(), It.IsAny<ClaimsPrincipal>(), It.IsAny<AuthenticationProperties>()))
            .Returns(Task.FromResult((object)null));

        var serviceProviderMock = new Mock<IServiceProvider>();
        serviceProviderMock
            .Setup(_ => _.GetService(typeof(IAuthenticationService)))
            .Returns(authServiceMock.Object);

        var controller = new HomeController(service.Object)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    RequestServices = serviceProviderMock.Object
                }
            }
        };
       
        var result =  await controller.Login(userAuthentication,"/") as RedirectResult;
        Assert.That(result.Url, Is.EqualTo("/"));

    }

    [Test]
    public async Task LoginPOST_WhenCalledFailed_LoginViewShouldRender()
    {
        var service = new Mock<IHomeService>();
       
        var userAuthentication = new UserAuthentication()
        {
            UserName = "Pra",
            Password = "123"
        };
       
        service.Setup(s => s.FindUserCredentials(userAuthentication)).Returns(new UserAuthentication
        {
            UserName = "Pra",
            Password = "12"
        });
        
        ITempDataProvider tempDataProvider = Mock.Of<ITempDataProvider>();
        TempDataDictionaryFactory tempDataDictionaryFactory = new TempDataDictionaryFactory(tempDataProvider);
        ITempDataDictionary tempData = tempDataDictionaryFactory.GetTempData(new DefaultHttpContext());

        var controller = new HomeController(service.Object)
        {
            TempData = tempData
        };
        var result =  await controller.Login(userAuthentication,"/") as ViewResult;
        Assert.That(result.ViewName, Is.EqualTo("Login"));

    }
    
    [Test]
    public async Task Logout_WhenCalledSuccess_RedirectToReturnURL()
    {
        var service = new Mock<IHomeService>();
       
        var userAuthentication = new UserAuthentication()
        {
            UserName = "Pra",
            Password = "123"
        };

        service.Setup(s => s.FindUserCredentials(userAuthentication)).Returns(userAuthentication);
        var authServiceMock = new Mock<IAuthenticationService>();
        authServiceMock
            .Setup(_ => _.SignOutAsync( It.IsAny<HttpContext>(),null,null))
            .Returns(Task.FromResult((object)null));

        var serviceProviderMock = new Mock<IServiceProvider>();
        serviceProviderMock
            .Setup(_ => _.GetService(typeof(IAuthenticationService)))
            .Returns(authServiceMock.Object);

        var controller = new HomeController(service.Object)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    // How mock RequestServices?
                    RequestServices = serviceProviderMock.Object
                }
            }
        };
       
        var result =  await controller.Logout() as RedirectResult;
        Assert.That(result.Url, Is.EqualTo("/"));

    }
    
}