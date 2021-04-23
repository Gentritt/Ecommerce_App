using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Data.Entities
{
	public class UsersStore:IdentityUser
	{

		public string FirstName { get; set; }
		public string LastName { get; set; }
	}
}
