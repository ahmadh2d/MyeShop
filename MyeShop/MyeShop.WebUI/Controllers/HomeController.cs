using MyeShop.Core.Contracts;
using MyeShop.Core.Models;
using MyeShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyeShop.WebUI.Controllers
{
	public class HomeController : Controller
	{
		public IRepository<Product> Context;
		public IRepository<ProductCategory> productCategories;

		public HomeController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryContext)
		{
			this.Context = productContext;
			this.productCategories = productCategoryContext;
		}

		public ActionResult Index(string category = null)
		{
			List<Product> products;
			List<ProductCategory> categories = this.productCategories.Collection().ToList();

			if (category == null)
			{
				products = Context.Collection().ToList();
			}
			else
			{
				products = Context.Collection().Where(p => p.Category == category).ToList();
			}

			ProductListViewModel plViewModel = new ProductListViewModel();
			plViewModel.Products = products;
			plViewModel.ProductCategories = categories;

			return View(plViewModel);
		}

		public ActionResult Details(string id)
		{
			Product product = Context.Find(id);
			if (product != null)
			{
				return View(product);
			}
			else
			{
				return HttpNotFound();
			}
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}