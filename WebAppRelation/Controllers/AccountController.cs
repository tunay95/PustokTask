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
                Name = registerVM.Name,
                Surname = registerVM.Surname
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
        [HttpPost]
        public async  Task<IActionResult> Login(LoginVM loginVM,string? returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await _userManager.FindByEmailAsync(loginVM.Email);
            if(user == null)
            {
                ModelState.AddModelError("", "Not Found");
                return View();
            }

            var result =  _signInManager.CheckPasswordSignInAsync(user,loginVM.Password,true).Result;
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Not Found");
                return View();
            }

            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Wait Please");
                return View();
            }

            await _signInManager.SignInAsync(user, false);
            return RedirectToAction("Home","Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Home","Home");
        }

  
        

    }
}
