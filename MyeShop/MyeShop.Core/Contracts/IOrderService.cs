using MyeShop.Core.Models;
using MyeShop.Core.ViewModels;
using System.Collections.Generic;

namespace MyeShop.Core.Contracts
{
	public interface IOrderService
	{
		void CreateOrder(Order order, List<BasketItemViewModel> basketItems);
	}
}
