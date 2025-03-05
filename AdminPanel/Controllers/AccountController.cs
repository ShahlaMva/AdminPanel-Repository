using AdminPanel.Helpers.Enum;
using AdminPanel.Models;
using AdminPanel.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

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


            return RedirectToAction("Index","Dashboard");
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
        //[HttpGet]
        //public async Task<IActionResult> CreateRole()
        //{
        //    foreach (var role in Enum.GetValues(typeof(Roles)))
        //    {
        //        if (!await _roleManager.RoleExistsAsync(role.ToString()))
        //        {
        //            await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
        //        }
        //    }
        //    return Ok();


        //}



        [Authorize(Roles="SuperAdmin")]
        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users;
            return View(users);
        }
        [HttpGet]
        public async Task<IActionResult> EditAccount(string id)
        {
            if (id is null) return NotFound();
            var register =await  _userManager.FindByIdAsync(id);
            if (register == null) return NotFound();
            var user = new RegisterVM
            {   Id=register.Id,
                UserName = register.UserName,
                Email=register.Email,
                FullName=register.FullName
            };

            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAccount(RegisterVM model)
        {
          
            if (ModelState["UserName"].ValidationState==ModelValidationState.Invalid||
                ModelState["Email"].ValidationState==ModelValidationState.Invalid) return View(model);
            var user = await _userManager.FindByIdAsync(model.Id);

            if (user is null) return NotFound();

            user.UserName = model.UserName;
            user.Email = model.Email;
            user.FullName = model.FullName;

            var result =await _userManager.UpdateAsync(user);
         
            return RedirectToAction("Index","Dashboard");
        }
        [HttpGet]
        public async Task<IActionResult> ChangePass(string id)
        {
            if (id is null) return NotFound();
           
            var user = await _userManager.FindByIdAsync(id);
            if(user is null)
            {
                return Unauthorized();
            }
            PasswordVM userVm = new PasswordVM
            {
                Id = user.Id,
               

            };


            return View(userVm);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePass(PasswordVM userVm)
        {
            if (!ModelState.IsValid)
            {
                return View(userVm);
            }
            var user = await _userManager.FindByIdAsync(userVm.Id);
            if (user is null) return NotFound();
            var result = await _userManager.ChangePasswordAsync(user, userVm.OldPassword, userVm.Password);
            
       
            return RedirectToAction("Index","Dashboard");
           }
       
        public async Task<IActionResult> DeleteAccount(string id)
        {
            if (id is null) return NotFound();
            var user = await _userManager.FindByIdAsync(id);
            if (user is null) return NotFound();
            await _userManager.DeleteAsync(user);
            return RedirectToAction("Index", "Dashboard");
        }


    }
}
