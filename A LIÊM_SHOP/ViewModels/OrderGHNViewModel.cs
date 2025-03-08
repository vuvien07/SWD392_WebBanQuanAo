﻿namespace A_LIÊM_SHOP.ViewModels
{
	public class OrderGHNViewModel
	{
		public int ShopId { get; set; }
		public string ToName { get; set; } = null!;
		public string ToPhone { get; set; } = null!;

		public string ToAddress { get; set; } = null!;

		public string ToWardName { get; set; } = null!;

		public string ToDistrictName { get; set; } = null!;
		public string ToProvinceName { get; set; } = null!;

		public int ServiceTypeId { get; set; }

		public int PaymentTypeId { get; set; }
		public string RequiredNote { get; set; } = null!;

		public DateTime CreateDate { get; set; } = DateTime.Now;

		public DateTime FinishDate { get; set; } = DateTime.Now;

		public string Status { get; set; } = null!;

		public Decimal CodAmount { get; set; }

	}
}
