using AutoMapper;
using CoreLoyalty.F5Seconds.Application.Features.Products.Commands.CreateProduct;
using CoreLoyalty.F5Seconds.Application.Features.Products.Queries.GetAllProducts;
using CoreLoyalty.F5Seconds.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLoyalty.F5Seconds.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Product, GetAllProductsViewModel>().ReverseMap();
            CreateMap<CreateProductCommand, Product>();
            CreateMap<GetAllProductsQuery, GetAllProductsParameter>();
        }
    }
}
