﻿using System.Text;
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
			_client.DefaultRequestHeaders.Add("token", "fa19bc40-fc2a-11ef-82e7-a688a46b55a3");
		}

		public async Task<List<OrderGHNViewModel>> GetAllOrders()
		{
			List<OrderGHNViewModel> results = new List<OrderGHNViewModel>();
			HttpResponseMessage response = await _client.GetAsync("https://dev-online-gateway.ghn.vn/shiip/public-api/v2/shipping-order/search");
			if (response.StatusCode == HttpStatusCode.OK)
			{
				string result = await response.Content.ReadAsStringAsync();
				JsonElement doc = JsonDocument.Parse(result).RootElement;
				JsonElement data = doc.GetProperty("data");
				JsonElement orderList = data.GetProperty("data");
				foreach (JsonElement item in orderList.EnumerateArray())
				{
					string createDate = item.GetProperty("created_date").GetString() ?? "";
					DateTime createdDate = DateTime.Parse(createDate);
					string toDate = item.GetProperty("finish_date").GetString() ?? "";
					DateTime finishDate = DateTime.Parse(createDate);
					results.Add(new OrderGHNViewModel
					{
						OrderCode = item.GetProperty("order_code").GetString() ?? "",
						CreateDate = createdDate,
						FinishDate = finishDate,
						ToName = item.GetProperty("to_name").GetString() ?? "",
						ToPhone = item.GetProperty("to_phone").GetString() ?? "",
						ToAddress = item.GetProperty("to_address").GetString() ?? "",
						Status = item.GetProperty("status").ToString(),
						CodAmount = item.GetProperty("cod_amount").GetDecimal(),
						RequiredNote = item.GetProperty("required_note").GetString() ?? "",
						Note = item.GetProperty("note").GetString() ?? "",
						ProductGHNs = GetProducts(item.GetProperty("items")) ?? new List<ProductGHNViewModel>()
					});
				}
			}

			return results;
		}


		public async Task<OrderGHNViewModel> GetOrderDetailByOrderCode(string orderCode)
		{
			var requestData = new StringContent(JsonSerializer.Serialize(new
			{
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
					OrderCode = data.GetProperty("order_code").GetString() ?? "",
					CreateDate = createdDate,
					FinishDate = finishDate,
					ToName = data.GetProperty("to_name").GetString() ?? "",
					ToPhone = data.GetProperty("to_phone").GetString() ?? "",
					ToAddress = data.GetProperty("to_address").GetString() ?? "",
					Status = data.GetProperty("status").ToString(),
					CodAmount = data.GetProperty("cod_amount").GetDecimal(),
					RequiredNote = data.GetProperty("required_note").GetString() ?? "",
					Note = data.GetProperty("note").GetString() ?? "",
					ProductGHNs = GetProducts(data.GetProperty("items")) ?? new List<ProductGHNViewModel>()
				};
			}
			return new OrderGHNViewModel();
		}

		private List<ProductGHNViewModel> GetProducts(JsonElement data)
		{
			List<ProductGHNViewModel> results = new List<ProductGHNViewModel>();
			foreach (JsonElement item in data.EnumerateArray())
			{
				results.Add(new ProductGHNViewModel
				{
					Name = item.GetProperty("name").GetString() ?? "",
					Quantity = item.GetProperty("quantity").GetInt32(),
					Code = item.GetProperty("code").GetString() ?? "",
					Weight = item.GetProperty("weight").GetInt32()
				});
			}
			return results;
		}

		public async Task<bool> UpdateOrderNoteById(string orderCode, string note)
		{
			var requestData = new StringContent(JsonSerializer.Serialize(new
			{
				note = note,
				order_code = orderCode

			}), Encoding.UTF8, "application/json");

			HttpResponseMessage response = await _client.PostAsync("https://dev-online-gateway.ghn.vn/shiip/public-api/v2/shipping-order/update", requestData);
			if (response.StatusCode == HttpStatusCode.OK)
			{
				string result = await response.Content.ReadAsStringAsync();
				JsonElement doc = JsonDocument.Parse(result).RootElement;
				return doc.GetProperty("code").GetInt32() == 200;
			}
			return false;
		}

		public async Task<bool> CancelOrderById(string orderCode)
		{
			var requestData = new StringContent(JsonSerializer.Serialize(new
			{
				order_codes = new string[] { orderCode }
			}), Encoding.UTF8, "application/json");
			HttpResponseMessage response = await _client.PostAsync("https://dev-online-gateway.ghn.vn/shiip/public-api/v2/switch-status/cancel", requestData);
			if (response.StatusCode == HttpStatusCode.OK)
			{
				string result = await response.Content.ReadAsStringAsync();
				JsonElement doc = JsonDocument.Parse(result).RootElement;
				return doc.GetProperty("code").GetInt32() == 200;
			}
			return false;
		}
	}
}
