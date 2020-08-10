using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyeShop.Core.Contracts;
using MyeShop.Core.Models;
using MyeShop.Core.ViewModels;
using MyeShop.DataAccess.InMemory;

namespace MyeShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        public IRepository<Product> Context;
        public IRepository<ProductCategory> productCategories;

        public ProductManagerController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryContext)
		{
            this.Context = productContext;
            this.productCategories = productCategoryContext;
		}

        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = this.Context.Collection().ToList();

            return View(products);
        }

        public ActionResult Create()
		{
            ProductManagerViewModel pmViewModel = new ProductManagerViewModel();

            pmViewModel.Product = new Product();
            pmViewModel.ProductCategories = productCategories.Collection();
            return View(pmViewModel);
		}

        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase file)
		{
            if (!ModelState.IsValid)
			{
                return View(product);
			}
			else
			{
                if (file != null)
				{
                    product.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + product.Image);
				}

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
                ProductManagerViewModel pmViewModel = new ProductManagerViewModel();

                pmViewModel.Product = product;
                pmViewModel.ProductCategories = productCategories.Collection();
                return View(pmViewModel);
            }
		}

        [HttpPost]
        public ActionResult Edit(Product product, string id, HttpPostedFileBase file)
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
                    if (file != null)
                    {
                        productToEdit.Image = product.Id + Path.GetExtension(file.FileName);
                        file.SaveAs(Server.MapPath("//Content//ProductImages//") + productToEdit.Image);
                    }

                    productToEdit.Name = product.Name;
                    productToEdit.Description = product.Description;
                    productToEdit.Category = product.Category;
                    productToEdit.Price = product.Price;

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