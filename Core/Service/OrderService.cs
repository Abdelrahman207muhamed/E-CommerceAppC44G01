using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models;
using DomainLayer.Models.OrderModule;
using Service.Specifications;
using ServiceAbstraction;
using Shared.Dtos.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class OrderService(IBasketRepository _basketRepository, IMapper _mapper , IUnitOfWork _unitOfWork ) : IOrderService
    {
        public async Task<OrderToReturnDto> CreateOrder(OrderDtos orderDtos, string Email)
        {
            //Map Address To OrdereAddress

            var OrderAddress = _mapper.Map<ShippingAddressDto, ShippingAddress>(orderDtos.Address);

            //Get Basket
            var Basket = await _basketRepository.GetBasketAsync(orderDtos.BasketId)
                ?? throw new BasketNotFoundException(orderDtos.BasketId);


            //Create Order List

            List<OrderItem> OrderItems = [];
            var ProductRepo = _unitOfWork.GetRepository<Product,int> ();
            foreach (var item in Basket.Items)
            {
                var Product = await ProductRepo.GetByIdAsync(item.Id)
                    ?? throw new ProductNotFoundException(item.Id);

                var orderItem = new OrderItem()
                {
                    Product = new ProductItemOrder()
                    {
                        ProductId = Product.Id,
                        ProductName = Product.Name,
                        PictureUrl = Product.PictureUrl
                    },
                    Price =  Product.Price,
                    Quantity = item.Quantity

                };
                OrderItems.Add(orderItem);

            }

            //Get Delivery Method 

            var DeliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(orderDtos.DeliveryMethodId)
                ?? throw new DeliveryMethodNotFoundException(orderDtos.DeliveryMethodId);

            //Calculate SubTotal

            var SubTotal = OrderItems.Sum(I => I.Quantity * I.Price);

            //Create Order

            var Order = new Order(Email, OrderAddress, DeliveryMethod, OrderItems, SubTotal);

            //Add Order
            await _unitOfWork.GetRepository<Order, Guid>().AddAsync(Order);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<Order, OrderToReturnDto>(Order);

        }

        public async Task<IEnumerable<DeliveryMethodDto>> GetAllDeliveryMethods()
        {
            var DeliveryMethods = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<DeliveryMethod>, IEnumerable<DeliveryMethodDto>>(DeliveryMethods);

        }

        public async Task<IEnumerable<OrderToReturnDto>> GetAllOrderAsync(string Email)
        {

            var Spec = new OrderSpecifications(Email);
            var Orders = await _unitOfWork.GetRepository<Order, Guid>().GetAllAsync(Spec);
            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderToReturnDto>>(Orders);
            
        }

        public async Task<OrderToReturnDto> GetOrderByIdAsync(Guid Id)
        {

            var Spec = new OrderSpecifications(Id);
            var Order = await _unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(Spec);

            return _mapper.Map<OrderToReturnDto>(Order);
        }
    }
}
