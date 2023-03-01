using Autoparts.Models;
using Autoparts.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Autoparts.Controllers
{
    public class AccountController:Controller
    {
        UserManager<AppUser> _userManager { get; }
        SignInManager<AppUser> _signInManager { get; }
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM userVM)
        {
            if (!ModelState.IsValid) return View();
            var user = await _userManager.FindByNameAsync(userVM.UsernameorEmail);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(userVM.UsernameorEmail);
                if (user == null)
                {
                    ModelState.AddModelError("", "username or password invalid");
                    return View();
                }
            }
            var result = await _signInManager.PasswordSignInAsync(user, userVM.Password, userVM.IsPersistance, true);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "username or password invalid");
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if(!ModelState.IsValid)return View();
            AppUser user = await _userManager.FindByNameAsync(registerVM.Name);
            if(user != null)
            {
                ModelState.AddModelError("Username", "Bu istifadeci movcuddur");
                return View();
            }
            user =new AppUser
            {
                Name = registerVM.Name,
                Surname=registerVM.SurName,
                UserName=registerVM.UserName,
                Email = registerVM.Email,
            };
            IdentityResult result =await _userManager.CreateAsync(user, registerVM.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();
            }
            _signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction("Index","Home");
        }
        public IActionResult MyAccount()
        {
            return View();
        }
    }
}
