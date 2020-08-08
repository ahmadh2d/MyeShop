using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyeShop.Core.Models;
using MyeShop.DataAccess.InMemory;

namespace MyeShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        public ProductRepository Context;

        public ProductManagerController()
		{
            this.Context = new ProductRepository();
		}

        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = this.Context.Collection().ToList();

            return View(products);
        }

        public ActionResult Create()
		{
            Product product = new Product();
            return View(product);
		}

        [HttpPost]
        public ActionResult Create(Product product)
		{
            if (!ModelState.IsValid)
			{
                return View(product);
			}
			else
			{
                this.Context.Insert(product);
                this.Context.Commit();
                return RedirectToAction("Index");
			}
		}

        public ActionResult Edit(String id)
		{
            Product product = Context.Find(id);
            if (product == null)
			{
                return HttpNotFound();
			}
            else
			{
                return View(product);
            }
		}

        [HttpPost]
        public ActionResult Edit(Product product, string id)
		{
            Product productToEdit = Context.Find(id);
            if (productToEdit != null)
			{
                if (!ModelState.IsValid)
				{
                    return View(product);
				}
                else
				{
                    productToEdit.Name = product.Name;
                    productToEdit.Description = product.Description;
                    productToEdit.Category = product.Category;
                    productToEdit.Price = product.Price;
                    productToEdit.Image = product.Image;

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
            Product productToDelete = Context.Find(id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string id)
        {
            Product productToDelete = Context.Find(id);
            if (productToDelete == null)
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