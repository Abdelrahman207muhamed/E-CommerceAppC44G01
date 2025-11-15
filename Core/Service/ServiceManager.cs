using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ServiceManager (IUnitOfWork unitOfWork ,IMapper mapper,IBasketRepository _basketRepository , UserManager<ApplicationUser> _userManager,IConfiguration _configuration) 
    {
        private readonly Lazy<ProductService> _LazyProductService = new Lazy<ProductService>(() => new ProductService(unitOfWork, mapper));
        private readonly Lazy<IBasketService> _LazyBasketService = new Lazy<IBasketService>(() => new BasketService(_basketRepository, mapper));
        private readonly Lazy<IAuthenticationService> _LazyAuthenticationService = new Lazy<IAuthenticationService>(()=>new AuthenticationService(_userManager,_configuration,mapper));
        private readonly Lazy<IOrderService> _LazyorderService = new Lazy<IOrderService>(() => new OrderService(_basketRepository, mapper, unitOfWork));
        public IProductService ProductService => _LazyProductService.Value;

        public IBasketService BasketService => _LazyBasketService.Value;

        public IAuthenticationService AuthenticationService => _LazyAuthenticationService.Value;

        public IOrderService OrderService => _LazyorderService.Value;
    }
}
