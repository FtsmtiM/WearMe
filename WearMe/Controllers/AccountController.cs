using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WearMe.ViewModels;

namespace WearMe.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _usermanager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _usermanager = userManager;
            _signInManager = signInManager;
        }
        // GET: Account
        public IActionResult LogIn(string returnUrl)
        {
            return View(new AccountViewModel()
            {
                ReturnUrl = returnUrl
            }) ;
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(AccountViewModel accountnViewModel)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(accountnViewModel);
            //}
            try
            {
                if(!ModelState.IsValid)
                {
                    return View(accountnViewModel);
                }
            }
            catch
            {
                throw;
            }
            var user = await _usermanager.FindByNameAsync(accountnViewModel.UserName);

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, accountnViewModel.Password,false,false);
                if (result.Succeeded)
                {
                    if (!String.IsNullOrEmpty(accountnViewModel.ReturnUrl) && Url.IsLocalUrl(accountnViewModel.ReturnUrl))
                    {
                        return Redirect(accountnViewModel.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    
                }
                ModelState.AddModelError("Password", "Incorrect Password, try again");
                return View(accountnViewModel);
            }
            ModelState.AddModelError("UserName", "User Not Found");
            return View(accountnViewModel);
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser()
                {
                    UserName = registerViewModel.UserName,
                    Email=registerViewModel.Email
                };
                var result = await _usermanager.CreateAsync(user, registerViewModel.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Loggedin", "Account");
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            //ModelState.AddModelError("Password", "Password must contain at least 6 characters, one digit,an uppercase and alphanumeric.");
            return View(registerViewModel);
        }

        public IActionResult LoggedIn()
        {
            return View();
        }
      
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}