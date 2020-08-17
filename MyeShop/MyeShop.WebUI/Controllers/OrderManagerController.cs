using MyeShop.Core.Contracts;
using MyeShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyeShop.WebUI.Controllers
{
	[Authorize(Roles = "Admin")]
	public class OrderManagerController : Controller
    {
        IOrderService OrderService;

		public OrderManagerController(IOrderService orderService)
		{
			this.OrderService = orderService;
		}

		// GET: OrderManager
		public ActionResult Index()
        {
			List<Order> orders = OrderService.GetOrderList();
			return View(orders);
        }

		public ActionResult UpdateOrder(string id)
		{
			ViewBag.StatusList = new List<string>()
			{
				"Order Created",
				"Payment Processed",
				"Order Shipped",
				"Order Completed"
			};
			Order order = OrderService.GetOrder(id);

			return View(order);
		}

		[HttpPost]
		public ActionResult UpdateOrder(Order updatedOrder, string id)
		{
			Order order = OrderService.GetOrder(id);

			order.OrderStatus = updatedOrder.OrderStatus;
			OrderService.UpdateOrder(order);

			return RedirectToAction("Index");
		}
	}
}