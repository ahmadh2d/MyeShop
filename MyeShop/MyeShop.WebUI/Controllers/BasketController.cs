using MyeShop.Core.Contracts;
using MyeShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyeShop.WebUI.Controllers
{
	public class BasketController : Controller
	{
        IBasketService BasketService;
        IOrderService OrderService;


		public BasketController(IBasketService basketService, IOrderService orderService)
		{
			this.OrderService = orderService;
            this.BasketService = basketService;
		}

		// GET: Basket
		public ActionResult Index()
        {
            var viewModel = BasketService.GetBasketItems(this.HttpContext);

            return View(viewModel);
        }

        public ActionResult AddToBasket(string id)
		{
            BasketService.AddToBasket(this.HttpContext, id);
            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromBasket(string id)
        {
            BasketService.RemoveFromBasket(this.HttpContext, id);
            return RedirectToAction("Index");
        }

        public ActionResult BasketSummary()
		{
            var viewModel = BasketService.GetBasketSummay(this.HttpContext);

            return PartialView(viewModel);
		}

        public ActionResult Checkout()
		{
            return View();
		}

        [HttpPost]
        public ActionResult Checkout(Order order)
		{
            var basketItems = BasketService.GetBasketItems(this.HttpContext);
            order.OrderStatus = "Order Created";

            // Process Payment 

            order.OrderStatus = "Payment Processed";
            OrderService.CreateOrder(order, basketItems);
            BasketService.ClearBasket(this.HttpContext);

            return RedirectToAction("ThankYou", new { OrderId = order.Id });
		}

        public ActionResult ThankYou(string OrderId)
		{
            ViewBag.OrderId = OrderId;

            return View();
		}
    }
}