using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
	public class LoginViewModel
	{
		[Required]
		public string Username { get; set; }
		[Required]
		public string Password { get; set; }
		[Display(Name = "Remember Me?")]
		public bool RemberMe { get; set; }

	}
}
