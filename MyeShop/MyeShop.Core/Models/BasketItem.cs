using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyeShop.Core.Models
{
	public class BasketItem : BaseEntity
	{
		public string BasketId { get; set; }
		public string ProductId { get; set; }
		public int Quantity { get; set; }
	}
}
