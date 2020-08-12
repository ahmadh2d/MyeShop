using MyeShop.Core.Contracts;
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

        public BasketController(IBasketService basketService)
		{
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
    }
}