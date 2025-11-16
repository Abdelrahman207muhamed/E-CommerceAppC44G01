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
                .ForMember(dist=>dist.productBrand,options=>options.MapFrom(src=>src.productBrand.Name))
                .ForMember(dist => dist.productType, options => options.MapFrom(src => src.productType.Name))
                .ForMember(dist=>dist.PictureUrl,options=>options.MapFrom<PictureUrlResolver>());


            CreateMap<ProductType, TypeDto>();      ////مطلبش مني اعمل ريفيرس ماب 
            CreateMap<ProductBrand, BrandDto>();    ////مطلبش مني اعمل ريفيرس ماب 
            //

            #endregion
            
            #region Basket

            CreateMap<CustomerBasket, BasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();

            #endregion

                      
            #region Order
            CreateMap<ShippingAddress, AddressDto>().ReverseMap();
            CreateMap<ShippingAddress, ShippingAddressDto>().ReverseMap();
            CreateMap<DeliveryMethod, DeliveryMethodDto>()
                .ForMember(dest => dest.Cost, options => options.MapFrom(src => src.Cost));
            ;

            CreateMap<OrderItem, OrderItemsDto>()
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.Product.ProductId))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.ProductName))
                .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.Product.PictureUrl));

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(dest => dest.Status, options => options.MapFrom(src => src.status.ToString()))
                .ForMember(dest => dest.DeliveryMethod, options => options.MapFrom(src => src.DeliveryMethod.ShortName))
                .ForMember(dest => dest.Total, options => options.MapFrom(src => src.subTotal + src.DeliveryMethod.Cost));




            #endregion


        }
    }
}
