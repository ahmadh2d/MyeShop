using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyeShop.Core.Contracts;
using MyeShop.Core.Models;
using MyeShop.Core.ViewModels;
using MyeShop.WebUI.Controllers;
using MyeShop.WebUI.Tests.Mocks;

namespace MyeShop.WebUI.Tests.Controllers
{
	[TestClass]
	public class HomeControllerTests
	{
		[TestMethod]
		public void TestMethod1()
		{
			IRepository<Product> productContext = new MockContext<Product>();
			IRepository<ProductCategory> productCategoryContext = new MockContext<ProductCategory>();

			productContext.Insert(new Product());

			HomeController controller = new HomeController(productContext, productCategoryContext);

			var result = controller.Index() as ViewResult;
			var viewModel = (ProductListViewModel)result.ViewData.Model;

			Assert.AreEqual(1, viewModel.Products.Count());
		}
	}
}
