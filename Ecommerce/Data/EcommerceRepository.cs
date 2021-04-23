using Ecommerce.Data.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Data
{
	public class EcommerceRepository : IEcommerceRepository
	{
		private readonly EcommerceContext _context;
		private readonly ILogger<IEcommerceRepository> _logger;

		public EcommerceRepository(EcommerceContext context, ILogger<IEcommerceRepository> logger)
		{
			_context = context;
			_logger = logger;
		}

		public void AddOrder(Order model)
		{
			_context.Add(model);
		}

		public IEnumerable<Order> GetAllOrders(bool IncludeItems)
		{
			try
			{
				if (IncludeItems)
				{
					_logger.LogInformation("Get All Orders was called");
					return _context.Orders
						.Include(p => p.items)
						.ThenInclude(p => p.Product)
						.ToList();
				}

				else
				{
					return _context.Orders.ToList();

				}
				
			}
			catch (Exception)
			{

				throw;
			}
		}

		public IEnumerable<Order> GetAllOrdersByUser(string username, bool IncludeItems)
		{
			try
			{
				if (IncludeItems)
				{
					_logger.LogInformation("Get All Orders was called");
					return _context.Orders
						.Where(o=> o.User.UserName == username)
						.Include(p => p.items)
						.ThenInclude(p => p.Product)
						.ToList();
				}

				else
				{
					return _context.Orders.Where(o => o.User.UserName == username).ToList();

				}

			}
			catch (Exception)
			{

				throw;
			}
		}

		public IEnumerable<Product> GetAllProducts()
		{
			try
			{
				_logger.LogInformation("Get All products was called");
				return _context.Products
					.OrderBy(p => p.Title)
					.ToList();
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to get products: {ex}");
				return null;
			}
			
		}

		public Order GetOrderById(string username, int id)
		{
			try
			{
				return _context.Orders
					.Include(p => p.items)
					.ThenInclude(p => p.Product)
					.Where(o => o.Id == id && o.User.UserName == username)
					.FirstOrDefault();
					
			}
			catch (Exception ex)
			{

				_logger.LogError($"Failed to get the order {ex}");
				return null;
			}
		}

		public IEnumerable<Product> GetProductByCategory(string category)
		{
			try
			{
				return _context.Products
				.Where(p => p.Category == category)
				.ToList();
			}
			catch (Exception ex)
			{

				_logger.LogError($"failed to get products by category {ex}");
				return null;
			}
			
		}

		public bool SaveAll()
		{
			return _context.SaveChanges() > 0;
		}
	}
}
