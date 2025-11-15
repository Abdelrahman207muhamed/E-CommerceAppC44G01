using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Models;
using DomainLayer.Models.BasketModule;
using DomainLayer.Models.IdentityModule;
using DomainLayer.Models.OrderModule;
using Shared.Dtos;
using Shared.Dtos.BasketDtos;
using Shared.Dtos.IdentityDtos;
using Shared.Dtos.OrderDtos;

namespace Service
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            #region Product
             
            CreateMap<Product, ProductDto>() //مطلبش مني اعمل ريفيرس ماب 
                .ForMember(dist=>dist.BrandName,options=>options.MapFrom(src=>src.productBrand.Name))
                .ForMember(dist => dist.TypeName, options => options.MapFrom(src => src.productType.Name))
                .ForMember(dist=>dist.PictureUrl,options=>options.MapFrom<PictureUrlResolver>());


            CreateMap<ProductType, TypeDto>();      ////مطلبش مني اعمل ريفيرس ماب 
            CreateMap<ProductBrand, BrandDto>();    ////مطلبش مني اعمل ريفيرس ماب 


            #endregion

            #region Basket

            CreateMap<CustomerBasket, BasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();

            #endregion

            #region Identity
            CreateMap<Address, AddressDto>().ReverseMap();

            #endregion

            #region Order
            CreateMap<ShippingAddressDto, ShippingAddress>().ReverseMap();

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(D => D.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d=>d.Total,o=>o.MapFrom(s=>s.GetTotal()));

            CreateMap<OrderItem, OrderItemsDto>()
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.ProductName))
                .ForMember(d=>d.PictureUrl,o=>o.MapFrom<OrderItemPictureUrlResolver>())
                .ForMember(d=>d.Price,o=>o.MapFrom(s=>s.Price))
                .ForMember(d=>d.Quantity,o=>o.MapFrom(s=>s.Quantity));

            CreateMap<DeliveryMethodDto, DeliveryMethod>();

            CreateMap<DeliveryMethod, DeliveryMethodDto>()
            .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryTime));




            #endregion

        }
    }
}
