using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using University.ViewModels;
using UniversityModel.Models;

namespace University.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]  
        public async Task<IActionResult> Register(RegisterUserViewModel newUserVM)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser userModel = new ApplicationUser();
                userModel.UserName = newUserVM.UserName;
                userModel.PasswordHash = newUserVM.Password;
                userModel.Address = newUserVM.Address;

               IdentityResult result= await userManager.CreateAsync(userModel,newUserVM.Password);
                if (result.Succeeded)
                {
                   await signInManager.SignInAsync(userModel, false);
                    return RedirectToAction("Index", "Student");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                }

                  
            }
            return View(newUserVM);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel userVM)
        {
            if (ModelState.IsValid) 
            {
               ApplicationUser userModel= await userManager.FindByNameAsync(userVM.UserName);
                if (userModel != null) 
                {
                  bool found=await userManager.CheckPasswordAsync(userModel, userVM.Password);
                    if (found)
                    {
                        await signInManager.SignInAsync(userModel, userVM.RememberMe);
                        return RedirectToAction("Index", "Student");
                    }
                }
                ModelState.AddModelError("", "Username and Password invalid");

            }

            return View(userVM);
        }



        public async Task<ActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");

        }
    }
}
