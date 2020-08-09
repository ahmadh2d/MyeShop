using System;
using System.ComponentModel.DataAnnotations;

namespace MyeShop.Core.Models
{
	public class ProductCategory
	{
		public string Id { get; set; }
		[Required]
		[StringLength(50)]
		[DataType(DataType.Text)]
		public string Category { get; set; }

		public ProductCategory()
		{
			this.Id = Guid.NewGuid().ToString();
		}
	}
}
