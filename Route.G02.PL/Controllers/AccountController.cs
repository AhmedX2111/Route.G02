﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Route.G02.DAL.Models;
using Route.G02.PL.ViewModels.Account;
using System.Threading.Tasks;

namespace Route.G02.PL.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		#region Sign Up
		public IActionResult SignUp()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SignUp(SignUpViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByNameAsync(model.UserName);
				if (user is null)
				{
					user = new ApplicationUser()
					{
						FName = model.FName,
						LName = model.LName,
						UserName = model.UserName,
						Email = model.Email,
						IsAgree = model.IsAgree

					};

					var result = await _userManager.CreateAsync(user, model.Password);
					if (result.Succeeded)
						return RedirectToAction(nameof(SignIn));

					foreach (var error in result.Errors)
						ModelState.AddModelError(string.Empty, error.Description);

				}

				ModelState.AddModelError(string.Empty, "This User Name is Already used before");

			}
			return View(model);
		}
		#endregion

		#region Sign In
		public IActionResult SignIn()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SignIn(SignInViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);
				if (user is not null)
				{
					var flag = await _userManager.CheckPasswordAsync(user, model.Password);
					if (flag)
					{
						var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
						if (result.IsLockedOut)
						{
							ModelState.AddModelError(string.Empty, "Your account is locked");
						}
						if (result.Succeeded)
						{
							return RedirectToAction(nameof(HomeController.Index), "Home");
						}
						if (result.IsNotAllowed)
						{
							ModelState.AddModelError(string.Empty, "Your account is not confirmed yet"); ;
						}

					}
				}
				ModelState.AddModelError(string.Empty, "Invalid login");
			}
			return View(model);

		}
		#endregion











	}
}