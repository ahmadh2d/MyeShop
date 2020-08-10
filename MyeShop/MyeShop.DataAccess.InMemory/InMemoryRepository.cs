using MyeShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;

namespace MyeShop.DataAccess.InMemory
{
	public class InMemoryRepository<T> where T : BaseEntity
	{
		MemoryCache cache = MemoryCache.Default;
		List<T> items;
		string className;

		public InMemoryRepository()
		{
			className = typeof(T).Name;
			items = cache[className] as List<T>;

			if (items == null)
			{
				items = new List<T>();
			}
		}

		public void Commit()
		{
			this.cache[className] = items;
		}

		public void Insert(T item)
		{
			this.items.Add(item);
		}

		public void Update(T item)
		{
			T itemToUpdate = this.items.Find(p => p.Id == item.Id);
			if (itemToUpdate != null)
			{
				itemToUpdate = item;
			}
			else
			{
				throw new Exception(className + " Not Found");
			}
		}

		public T Find(string id)
		{
			T item = this.items.Find(p => p.Id == id);
			if (item != null)
			{
				return item;
			}
			else
			{
				throw new Exception(className + " Not Found");
			}
		}

		public void Delete(string id)
		{
			T itemToDelete = this.items.Find(p => p.Id == id);
			if (itemToDelete != null)
			{
				items.Remove(itemToDelete);
			}
			else
			{
				throw new Exception(className + " Not Found");
			}
		}

		public IQueryable<T> Collection()
		{
			return items.AsQueryable();
		}

	}
}
