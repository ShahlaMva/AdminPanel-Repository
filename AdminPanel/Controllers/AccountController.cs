using AdminPanel.Helpers.Enum;
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
        private RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole>roleManager)
        {
            _userManager = userManager; 
            _signInManager = signInManager;
            _roleManager = roleManager;
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
                FullName=register.FullName,

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
            await _userManager.AddToRoleAsync(appUser, nameof(Roles.Member));
            await _signInManager.SignInAsync(appUser, true);


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
            if(!ModelState.IsValid) return View();
            AppUser user = await _userManager.FindByEmailAsync(login.UserNameOrEmail) ?? await _userManager.FindByNameAsync(login.UserNameOrEmail);
            if (user is null) {
                ModelState.AddModelError("", "Email or password incorrect");
                return View(); 
            }

           var signInResult = await _signInManager.PasswordSignInAsync(user, login.Password, login.RememberMe, true);

            if (!signInResult.Succeeded) {

                ModelState.AddModelError("", "Email or password incorrect");
                return View();
            }
            
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> CreateRole()
        {
            foreach (var role in Enum.GetValues(typeof(Roles)))
            {
                if (!await _roleManager.RoleExistsAsync(role.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
                }
            }
            return Ok();
           

        }




        public async Task<IActionResult> Index()
        {

            return View();
        }

      
    }
}
