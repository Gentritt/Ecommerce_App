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
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Controllers
{
	[Route("/api/orders/{orderid}/items")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class OrderItemsController: Controller
	{
		private readonly IEcommerceRepository _repository;
		private readonly ILogger<OrderItemsController> _logger;
		private readonly IMapper _mapper;

		public OrderItemsController(IEcommerceRepository repository, ILogger<OrderItemsController> logger,IMapper mapper)
		{
			_repository = repository;
			_logger = logger;
			_mapper = mapper;
		}

		[HttpGet]
		//This Method gets all the OrderItems for the given OrderID.
		public IActionResult Get(int orderid)
		{
			var username = User.Identity.Name;
			var order = _repository.GetOrderById(username,orderid);
			if (order != null) return Ok(_mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemViewModel>>(order.items));
			return NotFound();
		}

		[HttpGet("{id}")]
		//This method gets the specifiic OrderItem by the given ID.
		public IActionResult Get(int orderid, int id)
		{
			var username = User.Identity.Name;
			var order = _repository.GetOrderById(username,orderid);
			if(order!= null)
			{
				var item = order.items.Where(i => i.Id == id).FirstOrDefault();
				if(item != null)
				{
					return Ok(_mapper.Map<OrderItem, OrderItemViewModel>(item));
				}
			}
			return NotFound();

		}
	}
}
