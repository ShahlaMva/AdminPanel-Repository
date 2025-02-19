using AdminPanel.Models;
using AdminPanel.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager; 
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            if(!ModelState.IsValid)return View(register);

            AppUser appUser=new AppUser()
            {
                Email = register.Email,
                UserName=register.UserName,

            };

            IdentityResult identityResult =await _userManager.CreateAsync(appUser, register.Password);
            
            if (!identityResult.Succeeded)
            {
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(register);
            }
            await _signInManager.SignInAsync(appUser,true);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Login() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM login)
        {
            if(!ModelState.IsValid) return View(login);
            AppUser user=await _userManager.FindByEmailAsync(login.Email);
            if (user == null) {
                ModelState.AddModelError("", "Email or password incorrect");
                return View(login); 
            }

            Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.PasswordSignInAsync(user, login.Password, login.RememberMe, true);

            if (!signInResult.Succeeded) {

                ModelState.AddModelError("", "Email or password incorrect");
                return View(login);
            }
            
            return RedirectToAction("Index");
        }

    }
}
