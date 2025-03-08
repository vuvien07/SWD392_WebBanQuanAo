using A_LIÊM_SHOP.Models;
using A_LIÊM_SHOP.Services;
using A_LIÊM_SHOP.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;


namespace A_LIÊM_SHOP.Controllers
{
    public class OrderDetailsController : Controller
    {
        private readonly IOrderDetailsService _orderDetailsService;

        public OrderDetailsController(IOrderDetailsService orderDetailsService)
        {
            _orderDetailsService = orderDetailsService;
        }

        public void UpdateProductQuantity(OrderDetail orderDetail)
        {
            _orderDetailsService.UpdateProductQuantity(orderDetail);
        }

        public IActionResult Add(int id = 0, int orderId = 0)
        {
            var p = _orderDetailsService.GetProductById(id, orderId);

            p.Quantity += 1; // Tăng số lượng sản phẩm lên

            UpdateProductQuantity(p);

            return RedirectToAction("Detail", "Order", new { id = orderId });
        }

        public IActionResult Sub(int id = 0, int orderId = 0)
        {
            var p = _orderDetailsService.GetProductById(id, orderId);
            if (p.Quantity > 1)
            {
                p.Quantity -= 1; // Giam số lượng sản phẩm lên
                UpdateProductQuantity(p);
            }
            else
            {
                _orderDetailsService.RemoveProductById(id, orderId);
            }
           

            return RedirectToAction("Detail", "Order", new { id = orderId });
        }
    }
}
