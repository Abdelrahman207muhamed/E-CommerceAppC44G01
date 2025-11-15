using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ServiceManagerWithfactoryDelegate(Func<IProductService> ProductFactory) : IServiceManager
    {
        public IProductService ProductService => ProductFactory.Invoke();

        public IBasketService BasketService => 

        public IAuthenticationService AuthenticationService => 

        public IOrderService OrderService => 
    }
}
