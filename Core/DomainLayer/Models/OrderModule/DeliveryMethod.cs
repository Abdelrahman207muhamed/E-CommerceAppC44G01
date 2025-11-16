using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.OrderModule
{
    public class DeliveryMethod : BaseEntity<int>
    {
        public DeliveryMethod()
        {

        }
        public DeliveryMethod(string shortName, string description, decimal price, string deliveryTime)
        {
            ShortName = shortName;
            Description = description;
            Cost = price;
            DeliveryTime = deliveryTime;
        }
        public string ShortName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string DeliveryTime { get; set; } = string.Empty;
        public decimal Cost { get; set; } = default!;
    }
}
