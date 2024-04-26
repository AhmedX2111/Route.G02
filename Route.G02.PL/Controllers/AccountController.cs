using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Route.G02.DAL.Models;
using Route.G02.PL.Services.EmailSender;
using Route.G02.PL.ViewModels.Account;
using System.Threading.Tasks;

namespace Route.G02.PL.Controllers
{
    public class AccountController : Controller
    {
		private readonly IEmailSender _emailSender;
		private readonly IConfiguration _configuration;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(IEmailSender emailSender, IConfiguration configuration, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			_emailSender = emailSender;
			_configuration = configuration;
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
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var user = await _userManager.FindByEmailAsync(model.Email);
			if (user == null)
			{
				ModelState.AddModelError(string.Empty, "Invalid login");
				return View(model);
			}

			var passwordIsValid = await _userManager.CheckPasswordAsync(user, model.Password);
			if (!passwordIsValid)
			{
				ModelState.AddModelError(string.Empty, "Invalid login");
				return View(model);
			}

			var signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
			if (signInResult.Succeeded)
			{
				return RedirectToAction(nameof(HomeController.Index), "Home");
			}
			else if (signInResult.IsLockedOut)
			{
				ModelState.AddModelError(string.Empty, "Your account is locked");
			}
			else if (signInResult.IsNotAllowed)
			{
				ModelState.AddModelError(string.Empty, "Your account is not confirmed yet");
			}
			else
			{
				ModelState.AddModelError(string.Empty, "Invalid login");
			}

			return View(model);
		}

		public async new Task<IActionResult> SignOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction(nameof(SignIn));
		}
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendResetPasswordEmail(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
				var resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);
				if (user is not null)
                {
					var passwordUrl = Url.Action("ResetPassword", "Account", new { email = user.Email, token = resetPasswordToken }, "localhost:5001");
					//send email
					await _emailSender.SendAsync(
						from: _configuration["EmailSettings:SenderEmail"],
						recipients: model.Email,
						subject: "reset your password",
						body: passwordUrl
						);
					return Redirect(nameof(CheckYourInbox));
				}
                ModelState.AddModelError(string.Empty, "There is not account with this email");
            }
            return View(model);
        }
		public IActionResult CheckYourInbox()
		{
			return View();
		}
		[HttpGet]
		public IActionResult ResetPassword(string email, string token)
		{
			TempData["Email"] = email;
			TempData["token"] = token;
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var email = TempData["Email"] as string;
				var token = TempData["token"] as string;
				var user = await _userManager.FindByNameAsync(email);
				if (user is not null)
				{
					await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
					return RedirectToAction(nameof(SignIn));
				}
				ModelState.AddModelError(string.Empty, "Url is not valid");
			}
			return View(model);
		}
		#endregion











	}
}
