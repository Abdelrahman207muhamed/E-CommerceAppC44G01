using Shared.Dtos.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.OrderDtos
{
    public record OrderToReturnDto
    {
        public Guid Id { get; init; }
        public string buyerEmail { get; init; } = string.Empty;
        public AddressDto shipToAddress { get; set; }
        public ICollection<OrderItemsDto> Items { get; init; } = new List<OrderItemsDto>();
        public string Status { get; init; } = string.Empty;
        public string DeliveryMethod { get; init; } = string.Empty;
        public int DeliveryMethodId { get; init; }
        public decimal deliveryCost { get; init; }  
        public decimal SubTotal { get; init; }
        public DateTimeOffset OrederDate { get; init; } = DateTimeOffset.UtcNow;
        public string PaymentIntenId { get; init; } = string.Empty;
        public decimal Total { get; init; }
    }
}
