using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
	public class ProductViewModel
	{
		public int Id { get; set; }
		[Required]
		public string Category { get; set; }
		[Required]
		public string Size { get; set; }
		[Required]
		public string Title { get; set; }
		[Required]
		public string Artist { get; set; }
		[Required]
		public string ArtId { get; set; }
	}
}