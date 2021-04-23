 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
	public class AppController : Controller
	{
		private readonly IMailService _mailService;
		private readonly IEcommerceRepository _repository;

		public AppController(IMailService mailService, IEcommerceRepository repository)
		{
			_mailService = mailService;
			_repository = repository;
		}
		public IActionResult Index()
		{
			return View();
		}

		[HttpGet("contact")]
		public IActionResult Contact()
		{
			return View();
		}
		[HttpPost("Contact")]
		public IActionResult Contact(Contact model)
		{
			if (ModelState.IsValid)
			{
				_mailService.SendMessage("gentrit.selimi@riinvest.net", model.Subject, $"From : {model.Name}-{model.Email},Message {model.Message}");
			}
			ViewBag.UserMessage = "Message Sent";
			ModelState.Clear();

			return View();
		}

		public IActionResult About()
		{
			ViewBag.Title = "About US";
			return View();
		}

		[Authorize]
		public IActionResult Shop()
		{

			return View();
			//var result = _repository.GetAllProducts();
			//return View(result);
		}
	}
}
