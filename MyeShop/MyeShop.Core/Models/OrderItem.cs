using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyeShop.Core.Models
{
	public class OrderItem : BaseEntity
	{
		public string OrderId { get; set; }
		public string ProductId { get; set; }
		public string ProductName { get; set; }
		public string image { get; set; }
		public decimal Price { get; set; }
		public int Quantity { get; set; }
	}
}
