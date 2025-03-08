using A_LIÊM_SHOP.Models;

namespace A_LIÊM_SHOP.Repositories
{
    public interface IOrderDetailsRepository
    {
        void AddOrderDetail(OrderDetail orderDetail);
        void GetOrderDetailByOrderId(int orderId);
        void UpdateProductQuantity(OrderDetail orderDetail);

        OrderDetail GetProductById(int id, int orderId);

        void RemoveProductById(int id, int orderId);
    }
}
