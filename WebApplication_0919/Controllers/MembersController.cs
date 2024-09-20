using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication_0919.Models.EFModels.ViewModels;

namespace WebApplication_0919.Controllers
{
    public class MembersController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVm vm)
        {
            if (!ModelState.IsValid) return View(vm);

            string returnUrl = Request.Query["ReturnUrl"].ToString();
            if (string.IsNullOrWhiteSpace(returnUrl)) returnUrl = "/";

            if (vm.Account != "admin" || vm.Password != "123")
            {
                ModelState.AddModelError(string.Empty, "帳號或密碼錯誤");
                return View(vm);
            }

            // 將登入的 Claims 存入 ClaimsIdentity
            var claims = new List<Claim> 
            {
          new Claim(ClaimTypes.Name, vm.Account),
          new Claim("FullName", "Allen Kuo"),
          new Claim(ClaimTypes.Role, "Admin"), 
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            //todo登入
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);


            return Redirect(returnUrl);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
        
    }
}
