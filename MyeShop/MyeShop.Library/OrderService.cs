using MyeShop.Core.Contracts;
using MyeShop.Core.Models;
using MyeShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyeShop.Library
{
	public class OrderService : IOrderService
	{
		public IRepository<Order> OrderContext;

		public OrderService(IRepository<Order> orderContext)
		{
			this.OrderContext = orderContext;
		}

		public void CreateOrder(Order baseOrder, List<BasketItemViewModel> basketItems)
		{
			foreach(var item in basketItems)
			{
				baseOrder.OrderItems.Add(new OrderItem()
				{
					ProductId = item.Id,
					ProductName = item.ProductName,
					Price = item.Price,
					Quantity = item.Quantity,
					image = item.Image
				});
			}

			OrderContext.Insert(baseOrder);
			OrderContext.Commit();
		}

		public List<Order> GetOrderList()
		{
			return OrderContext.Collection().ToList();
		}

		public Order GetOrder(string Id)
		{
			Order order = OrderContext.Find(Id);
			return order;
		}

		public void UpdateOrder(Order updatedOrder)
		{
			OrderContext.Update(updatedOrder);
			OrderContext.Commit();
		}
	}
}
