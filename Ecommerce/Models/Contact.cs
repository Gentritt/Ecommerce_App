using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
	public class Contact
	{
	
		[Required]
		public string Name { get; set; }
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		[Required]
		public string Subject { get; set; }
		[Required]
		[MaxLength(250,ErrorMessage ="You can't write a message bigger than 250 characters.")]
		public string Message { get; set; }
	}
}
