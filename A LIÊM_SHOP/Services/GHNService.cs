using System.Text;
using A_LIÊM_SHOP.DTOs.Order;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System;
using A_LIÊM_SHOP.ViewModels;
using System.Net;
using System.Net.Http.Headers;

namespace A_LIÊM_SHOP.Services
{
	public class GHNService : IGHNService
	{
		private readonly HttpClient _client;

		public GHNService(HttpClient client)
		{
			_client = client;
		}
		public async Task<OrderGHNViewModel> GetOrderDetailByOrderCode(string orderCode)
		{
			_client.DefaultRequestHeaders.Add("token", "fa19bc40-fc2a-11ef-82e7-a688a46b55a3");
			var requestData = new StringContent(JsonSerializer.Serialize(new { 
				order_code = orderCode 
			}), Encoding.UTF8, "application/json");
			HttpResponseMessage response = await _client.PostAsync("https://dev-online-gateway.ghn.vn/shiip/public-api/v2/shipping-order/detail", requestData);
			if (response.StatusCode == HttpStatusCode.OK)
			{
				string result = await response.Content.ReadAsStringAsync();
				JsonElement doc = JsonDocument.Parse(result).RootElement;
				JsonElement data = doc.GetProperty("data");
				string createDate = data.GetProperty("created_date").GetString() ?? "";
				DateTime createdDate = DateTime.Parse(createDate);
				string toDate = data.GetProperty("finish_date").GetString() ?? "";
				DateTime finishDate = DateTime.Parse(createDate);
				return new OrderGHNViewModel
				{
					CreateDate= createdDate,
					FinishDate = finishDate,
					ToName = data.GetProperty("to_name").GetString() ?? "",
					ToPhone = data.GetProperty("to_phone").GetString() ?? "",
					ToAddress = data.GetProperty("to_address").GetString() ?? "",
					Status = data.GetProperty("status").ToString(),
					CodAmount = data.GetProperty("cod_amount").GetDecimal(),
					RequiredNote = data.GetProperty("required_note").GetString() ?? ""
				}; 
			}
			return new OrderGHNViewModel();
		}
	}
}
