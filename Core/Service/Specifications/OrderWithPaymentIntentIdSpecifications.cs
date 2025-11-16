using DomainLayer.Models.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    public class OrderWithPaymentIntentIdSpecifications :BaseSpecifications<Order,Guid>
    {
        public OrderWithPaymentIntentIdSpecifications(string PaymentIntentId):base(o=>o.PaymentIntentId==PaymentIntentId)
        {
            
        }

    }
}
