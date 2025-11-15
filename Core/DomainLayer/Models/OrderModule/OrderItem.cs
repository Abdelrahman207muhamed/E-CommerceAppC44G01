using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.OrderModule
{
    public class OrderItem :BaseEntity<int>
    {
        public ProductItemOrder Product { get; set; } = null!;
        public decimal Price   { get; set; }
        public int Quantity { get; set; }
    }
}
