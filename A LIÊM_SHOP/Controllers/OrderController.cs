using A_LIÊM_SHOP.Extensions;
using A_LIÊM_SHOP.Mapper;
using A_LIÊM_SHOP.Models;
using A_LIÊM_SHOP.Services;
using A_LIÊM_SHOP.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace A_LIÊM_SHOP.Controllers
{
	[Controller]
	[Route("order")]
	public class OrderController : Controller
	{
		private readonly IOrderService _orderService;
		private readonly IGHNService _ghnService;
		public OrderController(IOrderService orderService, IGHNService ghnService)
		{
			_orderService = orderService;
			_ghnService = ghnService;
		}
		[HttpGet]
		[Route("index")]
		public async Task<IActionResult> GetAllOrder()
		{
			
			List<OrderGHNViewModel> orders = await _ghnService.GetAllOrders();
			ViewBag.Orders = orders;
			return View("~/Views/Order/Index.cshtml");
		}
		[HttpGet]
		public async Task<IActionResult> Detail([FromQuery] string orderCode)
		{
			var order = await _ghnService.GetOrderDetailByOrderCode(orderCode);
			ViewBag.Order = order;
			return View("~/Views/Order/Detail.cshtml");
		}

       
        [HttpPost]
        public async Task<IActionResult> CancelOrder(string orderCode)
        {
            await _ghnService.CancelOrderById(orderCode);
            return RedirectToAction("GetAllOrder");
        }


    }
}
