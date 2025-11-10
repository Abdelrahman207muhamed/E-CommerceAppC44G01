using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Models;
using DomainLayer.Models.BasketModule;
using Shared.Dtos;
using Shared.Dtos.BasketDtos;

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
        }
    }
}
