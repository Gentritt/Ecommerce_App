using AutoMapper;
using Ecommerce.Data.Entities;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Data
{
	public class MappingProfile : Profile
	{

		public MappingProfile()
		{
			CreateMap<Order, OrderViewModel>()
				.ForMember(o=> o.OrderId, ex=> ex.MapFrom(o=> o.Id)).ReverseMap();

			CreateMap<OrderItem, OrderItemViewModel>().ReverseMap();
			CreateMap<Product, ProductViewModel>().ReverseMap();

		}

	}
}
