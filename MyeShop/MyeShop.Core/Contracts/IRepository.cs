using MyeShop.Core.Models;
using System.Linq;

namespace MyeShop.Core.Contracts
{
	public interface IRepository<T> where T : BaseEntity
	{
		IQueryable<T> Collection();
		void Commit();
		void Delete(string id);
		T Find(string id);
		void Insert(T item);
		void Update(T item);
	}
}