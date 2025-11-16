using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.OrderDtos
{
    public class OrderDtos
    {
        public string BasketId { get; set; } = null!;
        public int DeliveryMethodId { get; set; }
        public ShippingAddressDto ShipToAddress { get; set; } = null!;

    }
}
