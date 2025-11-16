using DomainLayer.Models.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    public class OrderSpecifications :BaseSpecifications<Order,Guid>
    {
        //Get All Order By Email
        public OrderSpecifications(string Email) : base(o => o.buyerEmail == Email)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.items);
            AddOrderByDescending(o => o.orderDate);


        }
        //Get OrderBy Id 
        public OrderSpecifications(Guid Id) : base(o => o.Id == Id)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.items);
        }

    }
}
