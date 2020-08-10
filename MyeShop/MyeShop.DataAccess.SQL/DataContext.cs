﻿using MyeShop.Core.Models;
using System.Data.Entity;

namespace MyeShop.DataAccess.SQL
{
	public class DataContext : DbContext
	{
		public DataContext() : base("DefaultConnection")
		{
		}

		public DbSet<Product> Products { get; set; }
		public DbSet<ProductCategory> ProductCategories { get; set; }
	}
}