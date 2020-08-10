using MyeShop.Core.Contracts;
using MyeShop.Core.Models;
using System.Data.Entity;
using System.Linq;

namespace MyeShop.DataAccess.SQL
{
	public class SQLRepository<T> : IRepository<T> where T : BaseEntity
	{
		internal DataContext context;
		internal DbSet<T> dbSet;

		public SQLRepository(DataContext context)
		{
			this.context = context;
			this.dbSet = context.Set<T>();
		}

		public IQueryable<T> Collection()
		{
			return dbSet;
		}

		public void Commit()
		{
			context.SaveChanges();
		}

		public void Delete(string id)
		{
			var item = this.Find(id);
			if (context.Entry(item).State == EntityState.Detached)
			{
				dbSet.Attach(item);
			}

			dbSet.Remove(item);
		}

		public T Find(string id)
		{
			return dbSet.Find(id);
		}

		public void Insert(T item)
		{
			dbSet.Add(item);
		}

		public void Update(T item)
		{
			dbSet.Attach(item);
			context.Entry(item).State = EntityState.Modified;
		}
	}
}
