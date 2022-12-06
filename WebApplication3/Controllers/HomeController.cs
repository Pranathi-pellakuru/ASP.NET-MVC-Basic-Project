using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApplication3.Models;
using WebApplication3.Services;

namespace WebApplication3.Controllers;

public class HomeController : Controller
{
    private readonly IHomeService _service;

    public HomeController(IHomeService service)
    {
        _service = service;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    

    public IActionResult Register()
    {
        return View("Register");
    }

    [HttpPost]
    public IActionResult Register(UserAuthentication userAuthentication)
    {
        _service.AddUserDetailsToDb(userAuthentication);
        return Redirect("/login?ReturnUrl=/");
    }
    
    [HttpGet("login")]
    public IActionResult Login( string? returnUrl)
    {
        ViewData["ReturnUrl"] = (returnUrl!=null)?returnUrl:"/";
        return View("Login");
    }
    
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(UserAuthentication obj , string? returnUrl)
    {
        if (returnUrl == null)
        {
            returnUrl = "/";
        }
        var details = _service.FindUserCredentials(obj);
        if (details.Password == obj.Password)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var claims = new List<Claim>();
            claims.Add(new Claim("username" , obj.UserName!));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, obj.Password!));
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme));
            // await AuthenticationHttpContextExtensions.SignInAsync(HttpContext, claimsPrincipal);
            await HttpContext.RequestServices.GetRequiredService<IAuthenticationService>()
                .SignInAsync(HttpContext, null, claimsPrincipal, null);
            // await HttpContext.SignInAsync(claimsPrincipal);
            return Redirect(returnUrl);
        }
        TempData["error"] = "Incorrect UserName or Password";
        return View("Login");
        }

    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return Redirect("/");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}