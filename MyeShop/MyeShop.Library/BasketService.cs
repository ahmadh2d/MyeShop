using MyeShop.Core.Contracts;
using MyeShop.Core.Models;
using MyeShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyeShop.Library
{
	public class BasketService : IBasketService
	{
		IRepository<Product> ProductContext;
		IRepository<Basket> BasketContext;
		public const string BasketSessionName = "eCommerceBasket";

		public BasketService(IRepository<Product> productContext, IRepository<Basket> basketContext)
		{
			this.ProductContext = productContext;
			this.BasketContext = basketContext;
		}

		private Basket GetBasket(HttpContextBase httpContext, bool createIfNull)
		{
			HttpCookie cookie = httpContext.Request.Cookies.Get(BasketSessionName);
			Basket basket = new Basket();

			if (cookie != null)
			{
				string basketId = cookie.Value;
				if (!string.IsNullOrEmpty(basketId))
				{
					basket = BasketContext.Find(basketId);
				}
				else
				{
					if (createIfNull)
					{
						basket = CreateNewBasket(httpContext);
					}
				}
			}
			else
			{
				if (createIfNull)
				{
					basket = CreateNewBasket(httpContext);
				}
			}

			return basket;
		}

		private Basket CreateNewBasket(HttpContextBase httpContext)
		{
			Basket basket = new Basket();
			BasketContext.Insert(basket);
			BasketContext.Commit();

			HttpCookie cookie = new HttpCookie(BasketSessionName);
			cookie.Value = basket.Id;
			cookie.Expires = DateTime.Now.AddDays(1);
			httpContext.Response.Cookies.Add(cookie);

			return basket;
		}

		public void AddToBasket(HttpContextBase httpContext, string productId)
		{
			Basket basket = GetBasket(httpContext, true);
			BasketItem basketItem = basket.BasketItems.FirstOrDefault(i => i.ProductId == productId);

			if (basketItem == null)
			{
				basketItem = new BasketItem()
				{
					BasketId = basket.Id,
					ProductId = productId,
					Quantity = 1
				};

				basket.BasketItems.Add(basketItem);
			}
			else
			{
				basketItem.Quantity += 1;
			}

			BasketContext.Commit();
		}

		public void RemoveFromBasket(HttpContextBase httpContext, string itemId)
		{
			Basket basket = GetBasket(httpContext, true);
			BasketItem basketItem = basket.BasketItems.FirstOrDefault(i => i.Id == itemId);

			if (basketItem != null)
			{
				basket.BasketItems.Remove(basketItem);
				BasketContext.Commit();
			}
		}

		public List<BasketItemViewModel> GetBasketItems(HttpContextBase httpContext, string productId)
		{
			Basket basket = GetBasket(httpContext, false);
			var result = (from b in basket.BasketItems
						  join p in ProductContext.Collection() on b.ProductId equals p.Id
						  select new BasketItemViewModel
						  {
							  Id = b.Id,
							  Quantity = b.Quantity,
							  ProductName = p.Name,
							  Price = p.Price,
							  Image = p.Image
						  }).ToList();

			return result;
		}

		public BasketSummaryViewModel GetBasketSummay(HttpContextBase httpContext)
		{
			Basket basket = GetBasket(httpContext, false);
			BasketSummaryViewModel viewModel = new BasketSummaryViewModel(0, 0);

			if (basket != null)
			{
				int? basketCount = (from b in basket.BasketItems
									select b.Quantity).Sum();
				decimal? basketTotal = (from b in basket.BasketItems
										join p in ProductContext.Collection() on b.ProductId equals p.Id
										select b.Quantity * p.Price).Sum();
				viewModel.BasketCount = basketCount ?? 0;
				viewModel.BasketTotal = basketTotal ?? decimal.Zero;

				return viewModel;
			}
			else
			{
				return viewModel;
			}
		}
	}
}
