using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.BasketModule;
using DomainLayer.Models.OrderModule;
using Microsoft.Extensions.Configuration;
using ServiceAbstraction;
using Shared.Dtos.BasketDtos;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductClass = DomainLayer.Models.Product;

namespace Service
{
    public class PaymentService(IConfiguration configuration,IBasketRepository _basketRepository ,IUnitOfWork _unitOfWork, IMapper mapper) : IPaymentService
    {
        public async Task<BasketDto> CreateOrUpdatePaymentIntentAsync(string basketId)
        {
            //[0] Install stripe.net
            //[1] Set up key [secret key] ==> stripe key
            StripeConfiguration.ApiKey = configuration.GetSection("StripeSettings")["SecretKey"];
            //[2] Get basket [by basketId]
            var basket = await GetBasketAsync(basketId);
            //[3] Validate items price ==> [basket.item.price = product.price] ==> product from db
            await ValidateBasketAsync(basket);
            // 4] Calculate Total amount
            var amount = CalculateTotalAsync(basket);
            // 5] Create or update paymentIntentId
            await CreationOrUpdatePaymentIntentAsync(basket, amount);
            // 6] Save changes [Update] Basket
            await _basketRepository.CreateOrUpdateBasketAsync(basket);
            // 7] Map to BasketDto.
            return mapper.Map<BasketDto>(basket);

        }

        private async Task CreationOrUpdatePaymentIntentAsync(CustomerBasket basket, long amount)
        {
            var stripeService = new PaymentIntentService();

            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                // create
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = amount,      // total = subtotal + shippingPrice
                    Currency = "USD",     // dollar
                    PaymentMethodTypes = ["card"]
                };

                var paymentIntent = await stripeService.CreateAsync(options);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = amount
                };

                await stripeService.UpdateAsync(basket.PaymentIntentId, options);
            }
        }

        private long CalculateTotalAsync(CustomerBasket basket)
        {
            var amount = (long)(basket.Items.Sum(i => i.Quantity * i.Price) + basket.ShippingPrice) * 100;
            return amount;
        }



        private async Task ValidateBasketAsync(CustomerBasket basket)
        {
            foreach (var item in basket.Items)
            {
                var product = await _unitOfWork.GetRepository<ProductClass, int>().GetByIdAsync(item.Id)
                    ?? throw new ProductNotFoundException(item.Id);

                item.Price = product.Price;
            }
            // Validate shipping Price too.
            if (!basket.DeliveryMethodId.HasValue) throw new DeliveryMethodNotFoundException("No delivery method selected");

            var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>()
                .GetByIdAsync(basket.DeliveryMethodId.Value)
                ?? throw new DeliveryMethodNotFoundException(basket.DeliveryMethodId.Value);

            basket.ShippingPrice = deliveryMethod.Cost;
        }

        private async Task<CustomerBasket> GetBasketAsync(string basketId)
        {
            return await _basketRepository.GetBasketAsync(basketId)
               ?? throw new BasketNotFoundException(basketId);
        }

        public Task UpdateOrderPaymentStatus(string request, string stripHeader)
        {
            throw new NotImplementedException();
        }
    }

}