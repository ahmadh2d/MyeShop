using MyeShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;

namespace MyeShop.DataAccess.InMemory
{
	public class ProductCategoryRepository
	{
		public MemoryCache cache = MemoryCache.Default;
		public List<ProductCategory> productCategories;

		public ProductCategoryRepository()
		{
			productCategories = cache["productCategories"] as List<ProductCategory>;
			if (productCategories == null)
			{
				productCategories = new List<ProductCategory>();
			}
		}

		public void Commit()
		{
			this.cache["productCategories"] = productCategories;
		}

		public void Insert(ProductCategory p)
		{
			this.productCategories.Add(p);
		}

		public void Update(ProductCategory productCategory)
		{
			ProductCategory productCategoryToUpdate = this.productCategories.Find(p => p.Id == productCategory.Id);
			if (productCategoryToUpdate != null)
			{
				productCategoryToUpdate = productCategory;
			}
			else
			{
				throw new Exception("Product Category Not Found");
			}
		}

		public ProductCategory Find(string id)
		{
			ProductCategory productCategory = this.productCategories.Find(p => p.Id == id);
			if (productCategory != null)
			{
				return productCategory;
			}
			else
			{
				throw new Exception("Product Category Not Found");
			}
		}

		public void Delete(string id)
		{
			ProductCategory productCategoryToDelete = this.productCategories.Find(p => p.Id == id);
			if (productCategoryToDelete != null)
			{
				productCategories.Remove(productCategoryToDelete);
			}
			else
			{
				throw new Exception("Product Category Not Found");
			}
		}

		public IQueryable<ProductCategory> Collection()
		{
			return productCategories.AsQueryable();
		}
	}
}
