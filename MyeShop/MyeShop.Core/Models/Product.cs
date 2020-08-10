using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyeShop.Core.Models
{
	public class Product : BaseEntity
	{
		[StringLength(20)]
		[DisplayName("Product Name")]
		public string Name { get; set; }
		public string Description { get; set; }

		[Range(0, 1000)]
		public decimal Price { get; set; }
		public string Category { get; set; }
		public string Image { get; set; }
	}
}
