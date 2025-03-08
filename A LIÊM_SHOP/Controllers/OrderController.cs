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
		public IActionResult Index()
		{
			User u = HttpContext.Session.GetObjectFromSession<User>("user");
			var orders = _orderService.GetByUserId(u.Id);
			return View(orders);
		}
		[HttpGet]
		[Route("detail")]
		public async Task<IActionResult> Detail([FromQuery] string orderCode)
		{
			var order = await _ghnService.GetOrderDetailByOrderCode(orderCode);
			OrderViewModel orderViewModel = OrderMapper.MapToOrderViewModel(order);
			ViewBag.Order = orderViewModel;
			return View("~/Views/Order/Detail.cshtml");
		}


	}
}
