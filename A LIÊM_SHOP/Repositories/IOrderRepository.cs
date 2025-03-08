using A_LIÊM_SHOP.Models;
using A_LIÊM_SHOP.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace A_LIÊM_SHOP.Repositories
{
    public interface IOrderRepository
    {
        public void AddOrder(Order order, List<Item> cart);
        public List<Order> GetAll();
        public List<Order> GetByUserId(int userId);

        Task<IActionResult> CreateOrder (Order order, List<Item> cart);
        public Order GetById(int id);
        public void UpdateOrder(Order order);
    }
}
