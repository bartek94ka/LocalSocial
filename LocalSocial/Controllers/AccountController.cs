using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using LocalSocial.Models;
using LocalSocial.Models.Bindings;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;

[Route("api/[controller]")]
public class AccountController : Controller
{

    //1
    private readonly UserManager<User> _securityManager;
    private readonly SignInManager<User> _loginManager;
    //1
    public AccountController(UserManager<User> secMgr, SignInManager<User> loginManager)
    {
        _securityManager = secMgr;
        _loginManager = loginManager;
    }

    //2

    [Route("register"), HttpPost]
    [AllowAnonymous]
    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> Register([FromBody] Register model)
    {
        if (ModelState.IsValid)
        {
            var user = new User
            {
                UserName = model.Email,
                Email = model.Email
            };
            var result = await _securityManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _loginManager.SignInAsync(user, isPersistent: false);
                return Json("Ok");
                //return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        return HttpBadRequest();
    }

    //3
    [Route("login")]
    [HttpPost]
    [AllowAnonymous]
    //public async Task<IActionResult> Login(Login model)
    public async Task<IActionResult> Login([FromBody] Login model)
    {
        if (ModelState.IsValid)
        {
            var result = await _loginManager.PasswordSignInAsync(model.Email, model.Password, false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return Ok();
            }
        }

        //jakąś lepszą odpowiedź przygotować
        return HttpNotFound();
    }

    //4
    [Route("logoff")]
    [HttpPost]
    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> LogOff()
    {
        await _loginManager.SignOutAsync();

        //return RedirectToAction(nameof(HomeController.Index), "Home");
        return null;
    }
}