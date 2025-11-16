using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.OrderModule
{
    public class Order :BaseEntity<Guid>
    {

        public Order()
        {
            
        }

        public Order(string _UserEmail, ShippingAddress _Address
            , DeliveryMethod _DeliveryMethod, ICollection<OrderItem> _Items, decimal _SubTotal,string _PaymentIntentId )
        {
            buyerEmail = _UserEmail;
            shipToAddress = _Address;
            DeliveryMethod = _DeliveryMethod;
            items = _Items;
            subTotal = _SubTotal;
            PaymentIntentId = _PaymentIntentId;
        }

        public string buyerEmail { get; set; } = null!;
        
        public DateTimeOffset orderDate { get; set; }= DateTimeOffset.Now;
        
        public OrderStatus OrderStatus { get; set; }
        
        public ShippingAddress shipToAddress { get; set; } = null!;
        
        public DeliveryMethod DeliveryMethod { get; set; } = null!;
       
        public int DeliveryMethodId { get; set; } //FK

        public string PaymentIntentId { get; set; } 

        public OrderStatus status { get; set; }

        public ICollection<OrderItem> items { get; set; } = [];

        public decimal subTotal { get; set; }

        public decimal deliveryCost => DeliveryMethod.Cost;
        

    }
}