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
        Task<BasketDto> CreateOrUpdatePaymentAsync(string BasketId);

    }
}
