using MyeShop.Core.ViewModels;
using System.Collections.Generic;
using System.Web;

namespace MyeShop.Core.Contracts
{
	public interface IBasketService
	{
		void AddToBasket(HttpContextBase httpContext, string productId);
		List<BasketItemViewModel> GetBasketItems(HttpContextBase httpContext);
		BasketSummaryViewModel GetBasketSummay(HttpContextBase httpContext);
		void RemoveFromBasket(HttpContextBase httpContext, string itemId);
	}
}