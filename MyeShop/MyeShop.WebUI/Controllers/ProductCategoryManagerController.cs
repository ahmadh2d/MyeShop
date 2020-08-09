using MyeShop.Core.Models;
using MyeShop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MyeShop.WebUI.Controllers
{
	public class ProductCategoryManagerController : Controller
	{
		// GET: ProductCategoryManager

		public ProductCategoryRepository Context;

		public ProductCategoryManagerController()
		{
			this.Context = new ProductCategoryRepository();
		}

		// GET: ProductManager
		public ActionResult Index()
		{
			List<ProductCategory> productCategories = this.Context.Collection().ToList();

			return View(productCategories);
		}

		public ActionResult Create()
		{
			ProductCategory productCategory = new ProductCategory();
			return View(productCategory);
		}

		[HttpPost]
		public ActionResult Create(ProductCategory productCategory)
		{
			if (!ModelState.IsValid)
			{
				return View(productCategory);
			}
			else
			{
				this.Context.Insert(productCategory);
				this.Context.Commit();
				return RedirectToAction("Index");
			}
		}

		public ActionResult Edit(String id)
		{
			ProductCategory productCategory = Context.Find(id);
			if (productCategory == null)
			{
				return HttpNotFound();
			}
			else
			{
				return View(productCategory);
			}
		}

		[HttpPost]
		public ActionResult Edit(ProductCategory productCategory, string id)
		{
			ProductCategory productCategoryToEdit = Context.Find(id);
			if (productCategoryToEdit != null)
			{
				if (!ModelState.IsValid)
				{
					return View(productCategory);
				}
				else
				{
					productCategoryToEdit.Category = productCategory.Category;

					Context.Commit();

					return RedirectToAction("Index");
				}
			}
			else
			{
				return HttpNotFound();
			}
		}

		public ActionResult Delete(string id)
		{
			ProductCategory productCategoryToDelete = Context.Find(id);
			if (productCategoryToDelete == null)
			{
				return HttpNotFound();
			}
			else
			{
				return View(productCategoryToDelete);
			}
		}

		[HttpPost]
		[ActionName("Delete")]
		public ActionResult ConfirmDelete(string id)
		{
			ProductCategory productCategoryToDelete = Context.Find(id);
			if (productCategoryToDelete == null)
			{
				return HttpNotFound();
			}
			else
			{
				Context.Delete(id);
				Context.Commit();
				return RedirectToAction("Index");
			}
		}
	}
}