using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
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
        public async Task<BasketDto> CreateOrUpdatePaymentAsync(string BasketId)
        {
            //Configure Stripe =>install Package Stripe
            StripeConfiguration.ApiKey = configuration["StripeSettings:SecretKey"];

            //Get Basket By BasketId 
            var Basket = await _basketRepository.GetBasketAsync(BasketId)
            ?? throw new BasketNotFoundException(BasketId);

            //Get Amount (Get Product + Delivery Method Cost )
            var ProductRepo =  _unitOfWork.GetRepository<ProductClass, int>();

            foreach (var Item in Basket.Items)
            {
                var Product = await ProductRepo.GetByIdAsync(Item.Id)
                ?? throw new ProductNotFoundException(Item.Id);
                Item.Price = Product.Price;

            }
            //-----------------
            //DeliveryMethod
            ArgumentNullException.ThrowIfNull(Basket.DeliveryMethodId);
            var DeliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(Basket.DeliveryMethodId.Value)
                ??throw new DeliveryMethodNotFoundException(Basket.DeliveryMethodId.Value) ;


            Basket.ShippingPrice = DeliveryMethod.Cost;

            var BasketAmount = (long)(Basket.Items.Sum(item => item.Quantity * item.Price) + DeliveryMethod.Cost) * 100;



            //Create Payment Intent [Create - Up date]

            var PaymentService = new PaymentIntentService();
            if (Basket.PaymentIntentId is null) //Create 
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = BasketAmount,
                    Currency = "USD",
                    PaymentMethodTypes = ["card"]
                };

                var PaymentIntent = await PaymentService.CreateAsync(options);
                Basket.PaymentIntentId = PaymentIntent.Id;
                Basket.ClientSecret = PaymentIntent.ClientSecret;
            }
            else 
            {
                //Update
                var Options = new PaymentIntentUpdateOptions()
                {
                    Amount = BasketAmount
                };
                await PaymentService.UpdateAsync(Basket.PaymentIntentId, Options);

            
            }
            await _basketRepository.CreateOrUpdateBasketAsync(Basket);
            return mapper.Map<BasketDto>(Basket);

        }
    }
}
