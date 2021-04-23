using Ecommerce.Data.Entities;
using System.Collections.Generic;

namespace Ecommerce.Data
{
	public interface IEcommerceRepository
	{
		IEnumerable<Product> GetAllProducts();
		IEnumerable<Product> GetProductByCategory(string category);
		IEnumerable<Order> GetAllOrders(bool IncludeItems);

		Order GetOrderById(string username, int id);

		bool SaveAll();
		void AddOrder(Order model);
		IEnumerable<Order> GetAllOrdersByUser(string username, bool IncludeItems);
	}
}