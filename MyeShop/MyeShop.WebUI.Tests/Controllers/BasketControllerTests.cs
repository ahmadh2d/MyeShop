using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyeShop.Core.Contracts;
using MyeShop.Core.Models;
using MyeShop.Core.ViewModels;
using MyeShop.Library;
using MyeShop.WebUI.Controllers;
using MyeShop.WebUI.Tests.Mocks;

namespace MyeShop.WebUI.Tests.Controllers
{
	[TestClass]
	public class BasketControllerTests
	{
		[TestMethod]
		public void CanAddBasketItems()
		{
			IRepository<Product> products = new MockContext<Product>();
			IRepository<Basket> baskets = new MockContext<Basket>();

			var httpContext = new MockHttpContext();

			IBasketService basketService = new BasketService(products, baskets);

			var controller = new BasketController(basketService);
			controller.ControllerContext = new System.Web.Mvc.ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);
			//basketService.AddToBasket(httpContext, "1");

			controller.AddToBasket("1");

			Basket basket = baskets.Collection().FirstOrDefault();

			Assert.IsNotNull(basket);
			Assert.AreEqual(1, basket.BasketItems.Count);
			Assert.AreEqual("1", basket.BasketItems.ToList().FirstOrDefault().ProductId);
		}

		[TestMethod]
		public void CanGetSummaryViewModel()
		{
			IRepository<Product> products = new MockContext<Product>();
			IRepository<Basket> baskets = new MockContext<Basket>();

			var httpContext = new MockHttpContext();

			products.Insert(new Product() { Id = "1", Price = 15.00m });
			products.Insert(new Product() { Id = "2", Price = 10.00m });

			Basket basket = new Basket();
			basket.BasketItems.Add(new BasketItem() { ProductId = "1", Quantity = 2 });
			basket.BasketItems.Add(new BasketItem() { ProductId = "2", Quantity = 1 });
			baskets.Insert(basket);

			IBasketService basketService = new BasketService(products, baskets);
			var controller = new BasketController(basketService);
			httpContext.Request.Cookies.Add(new System.Web.HttpCookie("eCommerceBasket") {Value = basket.Id });

			controller.ControllerContext = new System.Web.Mvc.ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);

			var result = controller.BasketSummary() as PartialViewResult;
			var basketSummary = (BasketSummaryViewModel) result.ViewData.Model;

			Assert.AreEqual(3, basketSummary.BasketCount);
			Assert.AreEqual(40.00m, basketSummary.BasketTotal);
		}
	}
}
