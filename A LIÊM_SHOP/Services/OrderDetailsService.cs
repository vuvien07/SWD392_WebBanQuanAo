using A_LIÊM_SHOP.Models;
using A_LIÊM_SHOP.Repositories;

namespace A_LIÊM_SHOP.Services
{
    public class OrderDetailsService : IOrderDetailsService
    {
        private readonly IOrderDetailsRepository _orderDetailsRepository;

        public OrderDetailsService(IOrderDetailsRepository orderDetailsRepository)
        {
            _orderDetailsRepository = orderDetailsRepository;
        }

        public OrderDetail GetProductById(int id, int orderId)
        {
           return _orderDetailsRepository.GetProductById(id, orderId);
        }

        public void RemoveProductById(int id, int orderId)
        {
            _orderDetailsRepository.RemoveProductById(id, orderId);
        }

        public void UpdateProductQuantity(OrderDetail orderDetail)
        {
            _orderDetailsRepository.UpdateProductQuantity(orderDetail);
        }

    }
}
