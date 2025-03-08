using A_LIÊM_SHOP.ViewModels;

namespace A_LIÊM_SHOP.Mapper
{
	public class OrderMapper
	{
		public static OrderViewModel MapToOrderViewModel(OrderGHNViewModel order)
		{
			OrderViewModel orderViewModel = new OrderViewModel();
			orderViewModel.Name = order.ToName;
			orderViewModel.Address = order.ToAddress;
			orderViewModel.Phone = order.ToPhone;
			return orderViewModel;
		}
	}
}
