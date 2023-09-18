using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers;

[ApiController]
[Route("Account")]
public class AccountController : ControllerBase
{
    public AccountController()
    {
        
    }

    [HttpPost("Login")]
    [AllowAnonymous]
    public async Task LogIn()
    {
        var claimList = new List<Claim>();
        var claim = new Claim("user", "daniel");
        var claim2 = new Claim("id", "simple");
        claimList.Add(claim);
        claimList.Add(claim2);
        var identity = new ClaimsIdentity(claimList, "SimpleUser");
        var user = new ClaimsPrincipal(identity);
        await HttpContext.SignInAsync("SimpleUser", user);
    }
    
    [HttpPost("LoginAdvanced")]
    [AllowAnonymous]
    public async Task LogInAdvanced()
    {
        var claimList = new List<Claim>();
        var claim = new Claim("user", "daniel avanzado");
        var claim2 = new Claim("id", "advanced");
        claimList.Add(claim);
        claimList.Add(claim2);
        var identity = new ClaimsIdentity(claimList, "AdvancedUser");
        var user = new ClaimsPrincipal(identity);
        await HttpContext.SignInAsync("AdvancedUser", user);
    }

    [HttpGet("Logout")]
    public void LogOut()
    {
        
    }

    [HttpGet("User")]
    [Authorize(Policy = "ValidSimpleUser")]
    public string User()
    {
        return HttpContext.User.Claims.First().Value;
    }
    
    [HttpGet("UserAdvanced")]
    [Authorize(Policy = "ValidAdvancedUser")]
    public string UserAdvanced()
    {
        return HttpContext.User.Claims.First().Value;
    }
}