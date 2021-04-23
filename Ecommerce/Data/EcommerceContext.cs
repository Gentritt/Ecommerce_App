﻿using Ecommerce.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Data
{
	public class EcommerceContext:IdentityDbContext<UsersStore>
	{
		public EcommerceContext(DbContextOptions<EcommerceContext> options)
			:base(options)
		{

		}

		public DbSet<Product> Products { get; set;}
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }
	}
}
