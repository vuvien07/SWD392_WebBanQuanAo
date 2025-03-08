using A_LIÊM_SHOP.ViewModels;

namespace A_LIÊM_SHOP.Services
{
	public interface IGHNService
	{
		 Task<OrderGHNViewModel> GetOrderDetailByOrderCode(string orderCode);
	}
}
