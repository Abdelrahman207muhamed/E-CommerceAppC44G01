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
    [ApiController]
    [Route("api/[controller]")]
    public class BasketsController(IServiceManager _serviceManager) :ControllerBase
    {
        //Get Basket
        [HttpGet]
        public async Task<ActionResult<BasketDto>> GetBasket(string Key)
        {
            var Basket = await _serviceManager.BasketService.GetBasketAsync(Key);
            return Ok(Basket);
        }


        //Create Or UpdateBasket
        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto basket)
        {
            var Basket = await _serviceManager.BasketService.CreateOrUpdateBasketAsync(basket);
            return Ok(Basket);
        }

        //Delete Basket
        [HttpDelete("{Key}")]
        public async Task<ActionResult<bool>> DeleteBasket(string Key)
        {
            var Result = await _serviceManager.BasketService.DeleteBasketAsync(Key);
            return Ok(Result);
        }
    }
}
