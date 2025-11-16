using Shared.Dtos.BasketDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IPaymentService
    {
        public Task<BasketDto> CreateOrUpdatePaymentIntentAsync(string BasketId);
        public Task UpdateOrderPaymentStatus(string request, string stripHeader);
    }
}
