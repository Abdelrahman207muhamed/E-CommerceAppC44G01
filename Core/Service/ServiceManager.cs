using AutoMapper;
using DomainLayer.Contracts;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ServiceManager (IUnitOfWork unitOfWork ,IMapper mapper) : IServiceManager
    {
        private readonly Lazy<ProductService> _productService = new Lazy<ProductService>(() => new ProductService(unitOfWork, mapper));

        public IProductService ProductService => _productService.Value;
    }
}
