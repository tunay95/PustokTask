using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAppRelation.ViewModels.Account;

namespace WebAppRelation.Controllers
{
    public class AccountController : Controller
    {
        UserManager<AppUser> _userManager;
        SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser user = new AppUser()
            {
                UserName = registerVM.Username,
                Email = registerVM.Email,
                Fullname = registerVM.FullName
            };
            var result = await _userManager.CreateAsync(user, registerVM.Password);
            if(!result.Succeeded) 
            {
                foreach(var item in result.Errors) 
                {
                    ModelState.AddModelError("", item.Description);

                }
                return View();
            }
            return RedirectToAction("Home", "Home");
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Logout()
        {
            return View();
        }

  
        

    }
}
