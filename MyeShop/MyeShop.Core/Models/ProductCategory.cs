using System.ComponentModel.DataAnnotations;

namespace MyeShop.Core.Models
{
	public class ProductCategory : BaseEntity
	{
		[Required]
		[StringLength(50)]
		[DataType(DataType.Text)]
		public string Category { get; set; }
	}
}
