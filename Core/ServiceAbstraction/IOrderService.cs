using Shared.Dtos.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IOrderService
    {
        Task<OrderToReturnDto> CreateOrder(OrderDtos orderDtos, string Email);

        Task<IEnumerable<DeliveryMethodDto>> GetAllDeliveryMethods();
        Task<IEnumerable<OrderToReturnDto>>GetAllOrderAsync(string Email);

        Task<OrderToReturnDto> GetOrderByIdAsync(Guid Id);

    }
}