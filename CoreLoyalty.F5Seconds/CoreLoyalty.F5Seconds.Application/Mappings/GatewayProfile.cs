using AutoMapper;
using CoreLoyalty.F5Seconds.Application.Common;
using CoreLoyalty.F5Seconds.Application.DTOs.F5seconds;
using CoreLoyalty.F5Seconds.Application.DTOs.GotIt;
using CoreLoyalty.F5Seconds.Application.DTOs.Urox;
using CoreLoyalty.F5Seconds.Application.Features.CoreLoyalty.DiaChis.ThanhPhos.Commands.CreateThanhPho;
using CoreLoyalty.F5Seconds.Application.Features.CoreLoyalty.DiaChis.ThanhPhos.Commands.GetAllThanhPhos;
using CoreLoyalty.F5Seconds.Application.Features.F5s.Commands.CreateTransaction;
using CoreLoyalty.F5Seconds.Application.Features.Urbox.Queries.GetVoucher;
using CoreLoyalty.F5Seconds.Domain.Entities;
using CoreLoyalty.F5Seconds.Domain.Entities.DiaChis;
using CoreLoyalty.F5Seconds.Domain.MemoryModels;
using System;
using VietCapital.Partner.F5Seconds.Application.Features.ThanhPhos.Queries.GetAllThanhPhos;
using static CoreLoyalty.F5Seconds.Application.DTOs.GotIt.GotItBuyVoucherRes;

namespace CoreLoyalty.F5Seconds.Application.Mappings
{
    public class GatewayProfile: Profile
    {
        public GatewayProfile()
        {
            #region Product destination
            CreateMap<F5sVoucherBase, Product>()
                .ForMember(d => d.ProductCode, m => m.MapFrom(s => $"F5S.{Helpers.RandomString(6)}"))
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
            #endregion

            #region Urbox destination
            CreateMap<CreateTransactionCommand, UrboxBuyVoucherReq>()
                .ForMember(d => d.productCode, m => m.MapFrom(s => s.productCode))
                .ForMember(d => d.transaction_id, m => m.MapFrom(s => s.transactionId))
                .ForMember(d => d.site_user_id, m => m.MapFrom(s => s.customerId))
                .ForMember(d => d.ttphone, m => m.MapFrom(s => s.customerPhone));
            CreateMap<F5sVoucherCode, UrboxTransactionResponse>()
                .ForMember(d => d.ExpiryDate, m => m.MapFrom(s => s.expiryDate))
                .ForMember(d => d.ProductCode, m => m.MapFrom(s => s.propductId))
                .ForMember(d => d.ProductPrice, m => m.MapFrom(s => s.productPrice))
                .ForMember(d => d.TransactionId, m => m.MapFrom(s => s.transactionId))
                .ForMember(d => d.VoucherCode, m => m.MapFrom(s => s.voucherCode))
                .ForMember(d => d.CustomerPhone, m => m.MapFrom(s => s.customerPhone))
                .ForMember(d => d.Created, m => m.MapFrom(s => DateTime.Now));
            CreateMap<UrboxBuyVoucherReq, UrboxTransactionRequest>()
                .ForMember(d => d.TransactionId, m => m.MapFrom(s => s.transaction_id))
                .ForMember(d => d.CustomerPhone, m => m.MapFrom(s => s.ttphone))
                .ForMember(d => d.PropductCode, m => m.MapFrom(s => s.productCode))
                .ForMember(d => d.Quantity, m => m.MapFrom(s => s.dataBuy[0].quantity))
                .ForMember(d => d.Created, m => m.MapFrom(s => DateTime.Now));
            #endregion

            #region GotIt destination
            CreateMap<CreateTransactionCommand, GotItBuyVoucherReq>()
               .ForMember(d => d.voucherRefId, m => m.MapFrom(s => s.transactionId))
               .ForMember(d => d.quantity, m => m.MapFrom(s => s.quantity))
               .ForMember(d => d.phone, m => m.MapFrom(s => s.customerPhone));
            CreateMap<CreateTransactionCommand, GotItTransactionRequest>()
                .ForMember(d => d.TransactionId, m => m.MapFrom(s => s.transactionId))
                .ForMember(d => d.CustomerId, m => m.MapFrom(s => s.customerId))
                .ForMember(d => d.ProductCode, m => m.MapFrom(s => s.productCode))
                .ForMember(d => d.Quantity, m => m.MapFrom(s => s.quantity))
                .ForMember(d => d.CustomerPhone, m => m.MapFrom(s => s.customerPhone))
                .ForMember(d => d.Created, m => m.MapFrom(s => DateTime.Now));
            CreateMap<GotItBuyVoucherReq, GotItTransactionRequest>()
                .ForMember(d => d.TransactionId, m => m.MapFrom(s => s.voucherRefId))
                .ForMember(d => d.CustomerPhone, m => m.MapFrom(s => s.phone))
                .ForMember(d => d.ProductCode, m => m.MapFrom(s => s.productCode))
                .ForMember(d => d.Quantity, m => m.MapFrom(s => s.quantity))
                .ForMember(d => d.Created, m => m.MapFrom(s => DateTime.Now));
            #endregion

            #region F5s destination
            CreateMap<UrboxVoucherDetailData, F5sVoucherDetail>()
                .ForMember(d => d.productId, m => m.MapFrom(s => s.data.id))
                .ForMember(d => d.productNm, m => m.MapFrom(s => s.data.title))
                .ForMember(d => d.productImg, m => m.MapFrom(s => s.data.image))
                .ForMember(d => d.productPrice, m => m.MapFrom(s => s.data.price))
                .ForMember(d => d.productTyp, m => m.MapFrom(s => s.data.type))
                .ForMember(d => d.productContent, m => m.MapFrom(s => s.data.content))
                .ForMember(d => d.productTerm, m => m.MapFrom(s => s.data.note))
                .ForMember(d => d.brandNm, m => m.MapFrom(s => s.data.brand))
                .ForMember(d => d.brandLogo, m => m.MapFrom(s => s.data.brandImage))
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
                .ForMember(d => d.storePhone, m => m.MapFrom(s => s.phone))
                .ForMember(d => d.storeLong, m => m.MapFrom(s => s.longitude))
                .ForMember(d => d.storeLat, m => m.MapFrom(s => s.latitude));
            CreateMap<GotItVoucherDetail, F5sVoucherDetail>()
                .ForMember(d => d.productContent, m => m.MapFrom(s => s.productDesc))
                .ForMember(d => d.productTerm, m => m.MapFrom(s => s.terms));
            CreateMap<GotItVoucherItem, F5sVoucherBase>();
            CreateMap<GotItVoucherDetailStore, F5sVoucherOffice>()
                .ForMember(d => d.storePhone, m => m.MapFrom(s => s.phone));
            CreateMap<VoucherInfoRes, F5sVoucherCode>()
                .ForMember(d => d.voucherCode, m => m.MapFrom(s => s.voucherCode))
                .ForMember(d => d.expiryDate, m => m.MapFrom(s => s.expiryDate));
            #endregion

            
        }
        
    }
}
