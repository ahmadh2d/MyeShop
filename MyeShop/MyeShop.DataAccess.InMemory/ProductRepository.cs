using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyeShop.Core.Models;

namespace MyeShop.DataAccess.InMemory
{
	class ProductRepository
	{
		public MemoryCache cache = MemoryCache.Default;
		public List<Product> products;

		public ProductRepository()
		{
			products = cache["products"] as List<Product>;
			if (products == null)
			{
				products = new List<Product>();
			}
		}

		public void Commit()
		{
			this.cache["products"] = products;
		}

		public void Insert(Product p)
		{
			this.products.Add(p);
		}

		public void Update(Product product)
		{
			Product productToUpdate = this.products.Find(p => p.Id == product.Id);
			if (productToUpdate != null)
			{
				productToUpdate = product;
			}
			else
			{
				throw new Exception("Product Not Found");
			}
		}

		public Product Find(string id)
		{
			Product product = this.products.Find(p => p.Id == id);
			if (product != null)
			{
				return product;
			}
			else
			{
				throw new Exception("Product Not Found");
			}
		}

		public void Delete(string id)
		{
			Product productToDelete = this.products.Find(p => p.Id == id);
			if (productToDelete != null)
			{
				products.Remove(productToDelete);
			}
			else
			{
				throw new Exception("Product Not Found");
			}
		}

		public IQueryable<Product> Collection()
		{
			return products.AsQueryable();
		}
	}
}
