using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Data.Entities
{
	public class Order
	{
		public int Id { get; set; }
		public DateTime OrderDate { get; set; }
		public string OrderNumber { get; set; }
		public ICollection<OrderItem> items { get; set; }
		public UsersStore User { get; set; }
		//public ICollection<Product> products { get; set; }
	}
}
