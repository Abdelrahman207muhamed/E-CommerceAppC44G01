using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.BasketModule
{
    public class CustomerBasket
    {
        public string Id { get; set; } = string.Empty;    //Guid : Created From Client [FronEnd]
        public ICollection<BasketItem> Items { get; set; } = [];
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        public decimal? ShippingPrice { get; set; }//DeliveryMethodId.Price
        public int? DeliveryMethodId { get; set; }
    }
}
