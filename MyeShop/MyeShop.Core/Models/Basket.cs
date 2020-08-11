﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyeShop.Core.Models
{
	public class Basket : BaseEntity
	{
		public virtual ICollection<BasketItem> BasketItems { get; set; }

		public Basket()
		{
			BasketItems = new List<BasketItem>();
		}
	}
}
