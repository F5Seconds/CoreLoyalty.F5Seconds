using AutoMapper;
using CoreLoyalty.F5Seconds.Application.DTOs.F5seconds;
using CoreLoyalty.F5Seconds.Application.DTOs.GotIt;
using CoreLoyalty.F5Seconds.Application.DTOs.Urox;
using CoreLoyalty.F5Seconds.Domain.Common;
using CoreLoyalty.F5Seconds.Domain.Entities;
using CoreLoyalty.F5Seconds.Domain.MemoryModels;
using System;
using System.Text;

namespace CoreLoyalty.F5Seconds.Application.Mappings
{
    public class GatewayProfile: Profile
    {
        public GatewayProfile()
        {
            CreateMap<UrboxVoucherDetailData, F5sVoucherDetail>()
                .ForMember(d => d.productId, m => m.MapFrom(s => s.data.id))
                .ForMember(d => d.productNm, m => m.MapFrom(s => s.data.title))
                .ForMember(d => d.productImg, m => m.MapFrom(s => s.data.image))
                .ForMember(d => d.productPrice, m => m.MapFrom(s => s.data.price))
                .ForMember(d => d.productTyp, m => m.MapFrom(s => s.data.type))
                .ForMember(d => d.productContent, m => m.MapFrom(s => s.data.content))
                .ForMember(d => d.productTerm, m => m.MapFrom(s => s.data.note))
                .ForMember(d => d.storeList, m => m.MapFrom(s => s.data.office));
            CreateMap<ItemVoucher, F5sVoucherBase>()
                .ForMember(d => d.productId, m => m.MapFrom(s => s.id))
                .ForMember(d => d.productNm, m => m.MapFrom(s => s.title))
                .ForMember(d => d.productImg, m => m.MapFrom(s => s.image))
                .ForMember(d => d.productPrice, m => m.MapFrom(s => s.price))
                .ForMember(d => d.productTyp, m => m.MapFrom(s => s.type))
                .ForMember(d => d.brandNm, m => m.MapFrom(s => s.brand_name))
                .ForMember(d => d.brandLogo, m => m.MapFrom(s => s.brandImage));
            CreateMap<UrboxVoucherOffice, F5sVoucherOffice>()
                .ForMember(d => d.storeAddr, m => m.MapFrom(s => s.address))
                .ForMember(d => d.storeLong, m => m.MapFrom(s => s.longitude))
                .ForMember(d => d.storeLat, m => m.MapFrom(s => s.latitude));
            CreateMap<GotItVoucherDetail, F5sVoucherBase>();
            CreateMap<GotItVoucherItem, F5sVoucherBase>();
            CreateMap<GotItVoucherDetailStore, F5sVoucherOffice>();
            CreateMap<F5sVoucherBase, Product>()
                .ForMember(d => d.Code, m => m.MapFrom(s => $"F5S.{Helpers.RandomString(6)}"))
                .ForMember(d => d.ProductId, m => m.MapFrom(s => s.productId))
                .ForMember(d => d.Status, m => m.MapFrom(s => true))
                .ForMember(d => d.Name, m => m.MapFrom(s => s.productNm))
                .ForMember(d => d.Price, m => m.MapFrom(s => s.productPrice))
                .ForMember(d => d.Type, m => m.MapFrom(s => s.productTyp))
                .ForMember(d => d.Size, m => m.MapFrom(s => s.productSize))
                .ForMember(d => d.Image, m => m.MapFrom(s => s.productImg))
                .ForMember(d => d.Partner, m => m.MapFrom(s => s.productPartner))
                .ForMember(d => d.BrandLogo, m => m.MapFrom(s => s.brandLogo))
                .ForMember(d => d.BrandName, m => m.MapFrom(s => s.brandNm))
                .ForMember(d => d.CreatedBy, m => m.MapFrom(s => "administrator@f5seconds.vn"))
                .ForMember(d => d.Created, m => m.MapFrom(s => DateTime.Now));
            CreateMap<Product, ProductMemory>();
        }
        
    }
}
