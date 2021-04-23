using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Data.Entities;
using Ecommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.Controllers
{
	public class AccountController : Controller
	{
		private readonly ILogger<AccountController> _logger;
		private readonly SignInManager<UsersStore>_signInManager;
		private readonly UserManager<UsersStore> _userManager;
		private readonly IConfiguration _config;

		public AccountController(ILogger<AccountController> logger, SignInManager<UsersStore> signInManager, UserManager<UsersStore> userManager,
			IConfiguration config
			)
		{
			_logger = logger;
			_signInManager = signInManager;
			_userManager = userManager;
			_config = config;
		}

		public IActionResult Login()
		{
			if (User.Identity.IsAuthenticated)
			{
				return RedirectToAction("Index", "App");
			}
			return View();

		}
		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RemberMe, false);
				if (result.Succeeded)
				{
				  RedirectToAction("Shop", "App");
				}
			}
			ModelState.AddModelError("", "Login Failed");
			
			return View();

		}

		[HttpGet]

		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();

			return RedirectToAction("Index", "App");
		}
		[HttpPost]
		public async Task<IActionResult> CreateToken([FromBody]LoginViewModel model) 
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByNameAsync(model.Username);
				if (user != null)
				{
					var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
					if (result.Succeeded)
					{
						//Create The token
						var claims = new[]
						{
							new Claim(JwtRegisteredClaimNames.Sub, user.Email),
							new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
							new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
						};

						var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"])); //Creates the token 
						var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
						var token = new JwtSecurityToken(
							_config["Tokens:Issuer"], //Who created the token, US
							_config["Tokens:Audience"], //who will use the token	
							claims,
							expires: DateTime.Now.AddMinutes(20),
							signingCredentials: creds
							);
						var results = new
						{
							token = new JwtSecurityTokenHandler().WriteToken(token),
							experiation = token.ValidTo

						};
						return Created("", results);
					}

				}
			}		

				return BadRequest();

		}
	}
}
