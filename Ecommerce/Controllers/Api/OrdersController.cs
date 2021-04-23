using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Data;
using Ecommerce.Data.Entities;
using Ecommerce.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Controllers.Api
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class OrdersController : Controller
	{
		private readonly IEcommerceRepository _repository;
		private readonly ILogger<OrdersController> _logger;
		private readonly IMapper _mapper;
		private readonly UserManager<UsersStore> _userManager;

		public OrdersController(IEcommerceRepository repository, ILogger<OrdersController> logger, IMapper mapper,
			UserManager<UsersStore> userManager)
		{
			_repository = repository;
			_logger = logger;
			_mapper = mapper;
			_userManager = userManager;
		}

		[HttpGet]
		public IActionResult GetOrders(bool IncludeItems = true)
		{
			try
			{
				var username = User.Identity.Name;
				var result = _repository.GetAllOrdersByUser(username,IncludeItems);
				return Ok(_mapper.Map<IEnumerable<Order>,IEnumerable<OrderViewModel>>(result));
			}
			catch (Exception ex)
			{

				_logger.LogError($"Error Getting the List of Orders, {ex}");
				return BadRequest();
			}
		}
		[HttpGet("{id:int}")]
		public IActionResult Get(int id)
		{
			try
			{
				var username = User.Identity.Name;
				var result = _repository.GetOrderById(username,id);
				if (result != null)
					return Ok(_mapper.Map<Order,OrderViewModel>(result));
				else return NotFound();
			}
			catch (Exception ex)
			{
				_logger.LogError($"Error getting the order {ex}");
				return BadRequest();
			}
		}

		[HttpPost]

		public async Task<IActionResult> Post([FromBody]OrderViewModel model)
		{
			
			try
			{
				if (ModelState.IsValid)
				{
					//var newOrder = new Order
					//{
					//	OrderDate = model.OrderDate,
					//	OrderNumber = model.OrderNumber,
					//	Id = model.OrderId
					//};

					var newOrder = _mapper.Map<OrderViewModel, Order>(model);

					if(newOrder.OrderDate == DateTime.MinValue)//If the OrderDate isnt specified.
					{ 
						newOrder.OrderDate = DateTime.Now;
					}

					var currentUser =await _userManager.FindByNameAsync(User.Identity.Name);
					newOrder.User = currentUser;
					_repository.AddOrder(newOrder);
					if (_repository.SaveAll())
					{
						//var vm = new OrderViewModel
						//{
						//	OrderId = newOrder.Id,
						//	OrderDate = newOrder.OrderDate,
						//	OrderNumber = newOrder.OrderNumber
						//};
						return Created($"/api/orders/{newOrder.Id}",_mapper.Map<Order,OrderViewModel>(newOrder));
					}
						
				}
				else
				{
					return BadRequest(ModelState); //Shows the errors when the data are sent
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"Error posting the order", ex);
			}
			return BadRequest();
		}
	}
}
