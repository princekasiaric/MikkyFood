using AutoMapper;
using MFR.Core.DTO.Request;
using MFR.DomainModels;
using MFR.DomainModels.Enum;
using MFR.Persistence.UnitOfWork;
using System;
using System.Threading.Tasks;

namespace MFR.Core.Service.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateOrderAsync(OrderRequest request)
        {
            var order = _mapper.Map<Order>(request);
            await _unitOfWork.Orders.CreateOrderAsync(order, _unitOfWork.ShoppingBaskets);

            if (Enum.TryParse(order.PaymentMethod.ToString(), true, out PaymentMethod paymentMethod))
            {
                switch (paymentMethod)
                {
                    case PaymentMethod.Cash:
                        order.PaymentMethod = PaymentMethod.Cash;
                        break;
                    case PaymentMethod.Card:
                        order.PaymentMethod = PaymentMethod.Card;
                        break;
                    default:
                        break;
                }
            }
            await CreateOrderDetailAsync(order);
            await SelectOrderMethodAsync(order);

            await _unitOfWork.Orders.AddOrderAsync(order);

            await _unitOfWork.CommitAndSaveChangesAsync();
        }

        private async Task CreateOrderDetailAsync(Order order)
        {
            var shoppingBasketItems = await _unitOfWork.ShoppingBaskets.GetShoppingBasketItemsAsync();

            foreach (var shoppingBasketItem in shoppingBasketItems)
            {
                var orderDetail = new OrderDetail
                {
                    Quantity = shoppingBasketItem.Quantity,
                    Price = shoppingBasketItem.SubMenu.Price,
                    SubMenuId = shoppingBasketItem.SubMenu.SubMenuId,
                    OrderId = order.OrderId
                };
                await _unitOfWork.OrderDetails.AddOrderDetailAsync(orderDetail);
            }
        }

        private async Task SelectOrderMethodAsync(Order order)
        {
            if (Enum.TryParse(order.OrderMethod.ToString(), true, out OrderMethod orderMethod))
            {
                switch (orderMethod)
                {
                    case OrderMethod.PickUp:
                        order.OrderMethod = OrderMethod.PickUp;
                        break;
                    case OrderMethod.Delivery:
                        order.OrderMethod = OrderMethod.Delivery;
                        break;
                    case OrderMethod.Reservation:
                        order.Reservation = new Reservation
                        {
                            Id = order.OrderId,
                            Date = order.Reservation.Date,
                            Time = order.Reservation.Time,
                            NumberOfPeople = order.Reservation.NumberOfPeople
                        };
                        await _unitOfWork.Reservations.AddReservationAsync(order.Reservation);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
