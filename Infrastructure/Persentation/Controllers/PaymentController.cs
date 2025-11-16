using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.Dtos.BasketDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persentation.Controllers
{
    public class PaymentController(IServiceManager serviceManager) :APIBaseController
    {
        [Authorize]
        [HttpPost("{BasketId}")]
        public async Task<ActionResult<BasketDto>> CreateOrUpdatePaymentIntentAsync(string BasketId)
        {
            var Basket = await serviceManager.PaymentService.CreateOrUpdatePaymentAsync(BasketId);
            return Ok(Basket);
        }
        
    }
}
