using A_LIÊM_SHOP.Models;

namespace A_LIÊM_SHOP.Services
{
    public interface IOrderDetailsService
    {
        void UpdateProductQuantity(OrderDetail orderDetail);
        OrderDetail GetProductById(int id, int orderId);

        void RemoveProductById(int id, int orderId);
    }
}
