using Ecommerce.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Data
{
	public class EcommerceSeeder
	{

		private readonly EcommerceContext _context;
		private readonly IWebHostEnvironment _hosting;
		private readonly UserManager<UsersStore> _userManager;

		public EcommerceSeeder(EcommerceContext context, IWebHostEnvironment hosting,UserManager<UsersStore> userManager)
		{
			_context = context;
			_hosting = hosting;
			_userManager = userManager;
		}

		public async Task Seed()
		{
			_context.Database.EnsureCreated();

			var user = await _userManager.FindByEmailAsync("gentselimi7@gmail.com");
			if(user== null)
			{
				user = new UsersStore()
				{
					FirstName = "Gentrit",
					LastName = "Selimi",
					UserName = "gentselimi7@gmail.com",
					Email = "gentselimi7@gmail.com"

				};
				var result = await _userManager.CreateAsync(user,"Gentking+1");
			}
			if (!_context.Products.Any())
			{
				var filepath = Path.Combine(_hosting.ContentRootPath, "Data/art.json");//Getting the list of products by Path.
				var json = File.ReadAllText(filepath);
				var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);
				_context.Products.AddRange(products);

				var order = new Order
				{
					OrderDate = DateTime.Now,
					OrderNumber = "1234",
					User = user,
					items = new List<OrderItem>()
					{
						new OrderItem()
						{
							Product =products.First(),
							Quantity = 5,
							UnitPrice = products.First().Price
						}
					}
				};
				_context.Orders.Add(order);
				_context.SaveChanges();
			}
		}


	}
}
