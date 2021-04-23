using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Data;
using Ecommerce.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Controllers.Api
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : Controller
	{
		private readonly IEcommerceRepository  _repository;
		private readonly ILogger<ProductsController> _logger;

		public ProductsController(IEcommerceRepository repository, ILogger<ProductsController> logger)
		{
			_repository = repository;
			_logger = logger;
		}

		[HttpGet]
		public IActionResult GetProducts()
		{
			try
			{
				return Ok(_repository.GetAllProducts());

			}
			catch (Exception ex)
			{

				_logger.LogError($"Failed to get products {ex}");
				return BadRequest("Failed to get products");
			}
			
		}

	}
}
